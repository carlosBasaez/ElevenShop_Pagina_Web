from django.shortcuts import render, HttpResponse, redirect
from suds import client
from suds.client import Client
from datetime import datetime
from django.views.decorators.csrf import csrf_protect, csrf_exempt, ensure_csrf_cookie

# Create your views here.
def home(request):
    return render(request, "main/index.html")

def carrito(request):
    print('>>> carrito')
    dicc = {}
    idcarrito = 'id_carrito'
    if idcarrito in request.session:
        print('>>>' + str(request.session[idcarrito]))
        servicio_productocantidadcarrito = Client('https://localhost:44344/WebService_ProductoCantidadCarrito.asmx?wsdl')
        lista = servicio_productocantidadcarrito.service.Listar(request.session[idcarrito])


        if lista:
            lista_producto = []
            for producto in lista[0]:
                dict_prod = {}
                dict_prod['datos'] = producto
                dict_prod['clp'] = GetCLP(producto.Valor)
                dict_prod['clp_total'] = GetCLP(producto.Valor * producto.Cantidad)
                lista_producto.append(dict_prod)

            dicc['lista_productos'] = lista_producto

            servicio_productocantidadcarrito = Client('https://localhost:44344/WebService_ProductoCantidadCarrito.asmx?wsdl')
            dicc['valortotal'] = GetCLP( servicio_productocantidadcarrito.service.ValorTotal(request.session[idcarrito]))
        else:
            servicio_carrito = Client('https://localhost:44344/WebService_carrito_compra.asmx?wsdl')
            servicio_productoCantidad = Client('https://localhost:44344/WebService_productoCantidad.asmx?wsdl')
            id = request.session['id_carrito']
            if not servicio_productoCantidad.service.borrar_producto_cantidad_carrito(id):
                print(">>> ERROR eliminar producto cantidad carrito")
            if not servicio_carrito.service.borrarCarrito(id):
                print(">>> ERROR eliminar carrito")
            del request.session['id_carrito']

    return render(request, "main/carrito_compras.html", dicc)

def carrito_reservar(request):

    servicio_carritos = Client('https://localhost:44344/WebService_carrito_compra.asmx?wsdl')
    servicio_pedidos = Client('https://localhost:44344/WebService_pedido.asmx?wsdl')

    rut = request.session['usuario_rut']
    id_carrito = request.session['id_carrito']
    id_estado = 0
    fecha = datetime.now()
    id_pedido = servicio_pedidos.service.UltimoPedidoID() + 1

    print(datetime.now())
    if (servicio_pedidos.service.insertarPedido(id_pedido, id_carrito, fecha, rut, id_estado)):
        del request.session['id_carrito']        
        return redirect('/pedidos/')

    return redirect('/carrito/')

def carrito_add_prod(request, id_producto):
    idcarrito = "id_carrito"
    id_carrito = 0

    servicio_productos = Client('https://localhost:44344/WebServices_Producto.asmx?wsdl')
    servicio_carritos = Client('https://localhost:44344/WebService_carrito_compra.asmx?wsdl')
    servicio_producto_cantidad = Client('https://localhost:44344/WebService_productoCantidad.asmx?wsdl')

    # revisar si existe un carrito de compras asociado a la sesion
    if idcarrito not in request.session:
        print(">>>>>  no hay carrito")
        # crear un carrito de compras y asociarlo
        id_carrito = servicio_carritos.service.GetNewID()
        carritoinfo = servicio_carritos.service.CrearCarrito(id_carrito, 0)

        print(">>>>> nuevo carro? "+str(carritoinfo))
        request.session[idcarrito] = id_carrito

    id_carrito = request.session[idcarrito]
    print(request.session[idcarrito])

    # revisar si existe un producto asociado
    if servicio_producto_cantidad.service.ExisteProductoCantidad(id_carrito, id_producto):
        ## actualizar producto cantidad
        print(">>>>>  actualizar producto")
        pc = servicio_producto_cantidad.service.GetProducto_Cantidad(id_carrito, id_producto)
        servicio_producto_cantidad.service.update_producto_cantidad(id_carrito, id_producto, pc.Cantidad + 1)
    else:
        ## crear nuevo producto cantidad
        print(">>>>>  insertar producto")
        prod = servicio_producto_cantidad.service.insertar_producto_cantidad(id_carrito, id_producto, 1)
        print(">>>> prod? " + str(prod))

    # lanzar al carrito de compras
    print('>>> fin carrito add')
    
    return redirect('/carrito/')

def carrito_remove_prod(request, id_producto):
    idcarrito = "id_carrito"

    if idcarrito in request.session:
        idcarrito = request.session[idcarrito]

        servicio_producto_cantidad = Client('https://localhost:44344/WebService_productoCantidad.asmx?wsdl')

        if servicio_producto_cantidad.service.ExisteProductoCantidad(idcarrito, id_producto):
            cantidad = servicio_producto_cantidad.service.GetProducto_Cantidad(idcarrito, id_producto).Cantidad
            cantidad = cantidad - 1

            if cantidad <= 0:
                servicio_producto_cantidad.service.borrar_producto_cantidad(idcarrito, id_producto)
            else:
                servicio_producto_cantidad.service.update_producto_cantidad(idcarrito, id_producto, cantidad)

    return redirect('/carrito/')

def carrito_delete_prod(request, id_producto):
    idcarrito = "id_carrito"

    if idcarrito in request.session:
        idcarrito = request.session[idcarrito]

        servicio_producto_cantidad = Client('https://localhost:44344/WebService_productoCantidad.asmx?wsdl')

        if servicio_producto_cantidad.service.ExisteProductoCantidad(idcarrito, id_producto):
            servicio_producto_cantidad.service.borrar_producto_cantidad(idcarrito, id_producto)

    return redirect('/carrito/')

def carrito_delete(request):

    if 'id_carrito' in request.session:
        servicio_carrito = Client('https://localhost:44344/WebService_carrito_compra.asmx?wsdl')
        servicio_productoCantidad = Client('https://localhost:44344/WebService_productoCantidad.asmx?wsdl')
        id = request.session['id_carrito']
        if not servicio_productoCantidad.service.borrar_producto_cantidad_carrito(id):
            print(">>> ERROR eliminar producto cantidad carrito")
        if not servicio_carrito.service.borrarCarrito(id):
            print(">>> ERROR eliminar carrito")
        del request.session['id_carrito']

    return redirect('/carrito/')

def inicio_sesion(request):

    if (request.method == 'POST'):
        print('rut: ' + request.POST['rut_field'])
        print('pass: ' + request.POST['pass_field'])

        servicio_usuario = Client('https://localhost:44344/WebService_Usuario.asmx?wsdl')
        usuario = servicio_usuario.service.getUsuarioSesion(str(request.POST['rut_field']), str(request.POST['pass_field']))

        if usuario != None:
            request.session['usuario_rut'] = usuario.rut
            request.session['usuario_nombre'] = usuario.nombrecompleto
            request.session['usuario_cliente'] = servicio_usuario.service.tipoCliente(usuario.rut)
            request.session['usuario_admin'] = servicio_usuario.service.tipoAdministrador(usuario.rut)
            request.session['usuario_bodeguero'] = servicio_usuario.service.tipoBodeguero(usuario.rut)
            request.session['usuario_despachador'] = servicio_usuario.service.tipoDespachador(usuario.rut)
            print(usuario)
            print(request.session)
            print(usuario.nombrecompleto)
            #del request.session['usuario']
            return redirect('/')
        else:
            #request.POST['email_field'] = ''
            return render(request, "main/inicio_sesion.html")
    
    else:
        return render(request, "main/inicio_sesion.html")

def cerrar_sesion(request):
    if 'usuario_rut' in request.session:
        del request.session['usuario_rut']
    if 'usuario_nombre' in request.session:
        del request.session['usuario_nombre']
    if 'usuario_cliente' in request.session:
        del request.session['usuario_cliente'] 
    if 'usuario_admin' in request.session:
        del request.session['usuario_admin']
    if 'usuario_bodeguero' in request.session: 
        del request.session['usuario_bodeguero'] 
    if 'usuario_despachador' in request.session:
        del request.session['usuario_despachador'] 
    
    #del request.session['id_carrito']

    return redirect('/')

def registrarse(request):

    #if esta registrado
    if 'usuario_rut' in request.session:
        return render(request, 'main/registrarse.html')    

    #if entra nuevo registro
    if request.method == 'POST':
        post = request.POST
        print(post)
        #print(post['rut'])
        nombre = post['nombre']
        email = post['email']
        rut = post['rut']
        password = post['password']
        telefono = post['fono']
        direccion = post['direccion']
        ciudad = post['ciudad']
        comuna = post['comuna']

        if post['password'] != post['password_confirmacion']:
            return render(request, 'main/registrarse.html')

        servicio_cliente = Client('https://localhost:44344/WebService_cliente.asmx?wsdl')
        respuesta_creacion_cliente = servicio_cliente.service.insertar_usuario(nombre, email, rut, password, telefono, direccion, ciudad, comuna)
        if respuesta_creacion_cliente == 'cliente_creado':
            request.session['usuario_rut'] = rut
            request.session['usuario_nombre'] = nombre
            return redirect('/')
        else:
            print(respuesta_creacion_cliente)

    #if aun no se registra
    return render(request, 'main/registrarse.html')

def cambiar_clave_pedir(request):
    if request.method == "POST":
        post = request.POST
        if "rut" in post:
            rut = post['rut']
            service_usuario = Client("https://localhost:44344/WebService_Usuario.asmx?wsdl")
            exist_usuario = service_usuario.service.existUsuario(rut)
            if exist_usuario:
                new_change_pass = service_usuario.service.NewChangePass(rut)
                if new_change_pass:
                    email = service_usuario.service.getUsuario(rut).email
                    dicc = {}
                    dicc["email"] = EmailHide(email)
                    return render(request, "main/cambiar_pass.html", dicc)

    return render(request, "main/cambiar_pass_pedir.html")

def cambiar_clave(request):
    if request.method == "POST":
        post = request.POST
        if "token" in post:
            token = post['token']
            pass1 = post['pass1']
            pass2 = post['pass2']

            if len(pass1) > 0 and pass1 == pass2:
                service_usuario = Client("https://localhost:44344/WebService_Usuario.asmx?wsdl")
                cambioCorrecto = service_usuario.service.ChangePass(token, pass1)
                if cambioCorrecto:
                    return redirect("/sesion/")

    return render(request, "main/cambiar_pass.html")

def usuario_lista(request, tipo):

    if 'usuario_admin' not in request.session or not request.session['usuario_admin']:
        return redirect('/')

    dicc = {}
    dicc['title'] = "Lista de Usuarios"
    dicc['tipo'] = tipo
    #iniciar buscar
    if tipo == "admin":
        servicio_admin = Client('https://localhost:44344/WebService_Administrador.asmx?wsdl')
        try:
            usuarios = servicio_admin.service.listarUsuario()[0]
            if len(usuarios) > 0:
                dicc['usuarios'] = usuarios
        except:
            pass
        dicc['title'] = "Lista de Usuarios Administradores"
    elif tipo == "bodeguero":
        servicio_bodega = Client('https://localhost:44344/WebService_Bodeguero.asmx?wsdl')
        try:
            usuarios = servicio_bodega.service.listarUsuario()[0]
            if len(usuarios) > 0:
                dicc['usuarios'] = usuarios
        except:
            pass
        dicc['title'] = "Lista de Usuarios Bodegueros"
    elif tipo == "despachador":
        servicio_despachador = Client('https://localhost:44344/WebService_Despachador.asmx?wsdl')
        try:
            usuarios = servicio_despachador.service.listarUsuario()[0]
            if len(usuarios) > 0:
                dicc['usuarios'] = usuarios
        except:
            pass
        dicc['title'] = "Lista de Usuarios Despachadores"
    else:
        servicio_usuario = Client('https://localhost:44344/WebService_Usuario.asmx?wsdl')
        try:
            usuarios = servicio_usuario.service.listar()[0]
            if len(usuarios) > 0:
                dicc['usuarios'] = usuarios
        except:
            pass
    #finalizar buscar

    #iniciar filtro
    if request.method == "POST":
        post = request.POST
        rut = post['rut']
        if 'usuarios' in dicc:
            usuarios_filtrado = []
            for user in dicc['usuarios']:
                if rut in user.rut:
                    usuarios_filtrado.append(user)
            if len(usuarios_filtrado) > 0:
                dicc['usuarios'] = usuarios_filtrado
            else:
                del dicc['usuarios']


    #finalizar filtro

    return render(request, 'main/usuario_lista.html', dicc)    

def usuario_perfil(request, rut):

    if 'usuario_admin' not in request.session or not request.session['usuario_admin']:
        return redirect('/')

    dicc = {}
    
    servicio_usuario = Client('https://localhost:44344/WebService_Usuario.asmx?wsdl')
    servicio_cliente = Client('https://localhost:44344/WebService_cliente.asmx?wsdl')
    servicio_admin = Client('https://localhost:44344/WebService_Administrador.asmx?wsdl')
    servicio_bodeguero = Client('https://localhost:44344/WebService_Bodeguero.asmx?wsdl')
    servicio_despachador = Client('https://localhost:44344/WebService_Despachador.asmx?wsdl')

    #guardar datos
    if request.method == "POST":
        post = request.POST
        print(post)
        #usuario
        rut = post['rut']
        nombre = post['nombre']
        email = post['email']
        telefono = post['telefono']
        password = post['password']
        if servicio_usuario.service.existUsuario(rut):
            servicio_usuario.service.updateUsuario(nombre, email, rut, telefono)
        else:
            servicio_usuario.service.insertUsuario(nombre, email, rut, password, telefono)

        #cliente
        direccion = post['direccion']
        ciudad = post['ciudad']
        comuna = post['comuna']
        if 'isCliente' in post:
            if servicio_cliente.service.exist(rut):
                servicio_cliente.service.update(rut, direccion, ciudad, comuna)
            else:
                servicio_cliente.service.insert(rut, direccion, ciudad, comuna)
        elif servicio_cliente.service.exist(rut):
            servicio_cliente.service.delete(rut)

        #administrador
        if 'isAdministrador' in post:
            if servicio_admin.service.exist(rut):
                pass
            else:
                servicio_admin.service.insert(rut)
        elif servicio_admin.service.exist(rut):
            servicio_admin.service.delete(rut)

        #bodeguero
        if 'isBodeguero' in post:
            if servicio_bodeguero.service.exist(rut):
                pass
            else:
                servicio_bodeguero.service.insert(rut)
        elif servicio_bodeguero.service.exist(rut):
            servicio_bodeguero.service.delete(rut)

        #despachador
        if 'isDespachador' in post:
            if servicio_despachador.service.exist(rut):
                pass
            else:
                servicio_despachador.service.insert(rut)
        elif servicio_despachador.service.exist(rut):
            servicio_despachador.service.delete(rut)

    #cargar datos en la web
    dicc['rut'] = rut
    if servicio_usuario.service.existUsuario(rut):
        #usuario
        usuario = servicio_usuario.service.getUsuario(rut)
        dicc['usuario'] = usuario
        #cliente
        cliente = servicio_cliente.service.exist(rut)
        if client:
            cliente = servicio_cliente.service.get(rut)
            dicc['cliente'] = cliente
        #admin
        admin = servicio_admin.service.exist(rut)
        if admin:
            admin = servicio_admin.service.get(rut)
            dicc['administrador'] = admin
        #bodeguero
        bodeguero = servicio_bodeguero.service.exist(rut)
        if bodeguero:
            bodeguero = servicio_bodeguero.service.get(rut)
            dicc['bodeguero'] = bodeguero
        #despachador
        despachador = servicio_despachador.service.exist(rut)
        if despachador:
            despachador = servicio_despachador.service.get(rut)
            dicc['despachador'] = despachador

    return render(request, 'main/usuario_detalle.html', dicc)    

def producto(request, id_producto):
    dicc = {}
    
    url = 'https://localhost:44344/WebServices_Producto.asmx?wsdl'
    client = Client(url)

    producto = client.service.MostrarProducto(id_producto)

    dicc['valor'] = GetCLP(producto.Valor)

    dicc['producto'] = producto
    dicc['nombre'] = producto.Nombre
    dicc['caracteristicas'] = producto.Caracteristicas

    return render(request, "main/producto.html", dicc)

def producto_lista(request):
    dicc = {}
    service_producto = Client("https://localhost:44344/WebServices_Producto.asmx?wsdl")
    productos = service_producto.service.Listar()[0]

    #print(productos)
    dicc["productos"] = productos

    return render(request, "main/mantenedor_productos_lista.html", dicc)

def producto_detalle(request, idproducto):
    dicc = {}
    service_producto = Client("https://localhost:44344/WebServices_Producto.asmx?wsdl")

    if request.method == "POST":
        post = request.POST
        nombre = post["nombre"]
        stock = post["stock"]
        valor = post["valor"]
        caracteristicas = post["caracteristicas"]
        service_producto.service.update(idproducto, nombre, stock, valor, caracteristicas)

    prod = service_producto.service.MostrarProducto(idproducto)
    dicc["producto"] = prod

    return render(request, "main/mantenedor_producto.html", dicc)

def pedido_usuario(request):

    dicc = {}
    servicio_pedidos = Client('https://localhost:44344/WebService_pedido.asmx?wsdl')
    servicio_estado = Client('https://localhost:44344/Web_service_estados.asmx?wsdl')
    servicio_productocantidadcarrito = Client('https://localhost:44344/WebService_ProductoCantidadCarrito.asmx?wsdl')


    pedidos = servicio_pedidos.service.MostrarPedidos(request.session['usuario_rut'])
    if (pedidos):
        print(pedidos)

        dicc['pedidos'] = pedidos[0]

        lista_pedidos = []
        
        for p in pedidos[0]:
            tupla = (p, GetCLP(servicio_productocantidadcarrito.service.ValorTotal(p.Id_carrito)))
            lista_pedidos.append(tupla)
        dicc['lista_pedidos'] = lista_pedidos
        print(lista_pedidos)

    return render(request, "main/pedido_usuario.html", dicc)

def pedido_detalle(request, id_pedido):
    dicc = {}
    
    #info pedido
    servicio_pedidos = Client('https://localhost:44344/WebService_pedido.asmx?wsdl')
    pedido = servicio_pedidos.service.pedido_por_id(id_pedido)
    dicc['pedido'] = pedido

    #info productos
    servicio_productos = Client('https://localhost:44344/WebServices_Producto.asmx?wsdl')
    servicio_carritos = Client('https://localhost:44344/WebService_carrito_compra.asmx?wsdl')
    servicio_producto_cantidad = Client('https://localhost:44344/WebService_productoCantidad.asmx?wsdl')
    servicio_productocantidadcarrito = Client('https://localhost:44344/WebService_ProductoCantidadCarrito.asmx?wsdl')
    lista = servicio_productocantidadcarrito.service.Listar(pedido.Id_carrito)
    dicc['lista_productos'] = lista[0]

    servicio_productocantidadcarrito = Client('https://localhost:44344/WebService_ProductoCantidadCarrito.asmx?wsdl')
    dicc['valortotal'] = GetCLP( servicio_productocantidadcarrito.service.ValorTotal(id_pedido))

    return render(request, "main/pedido_detalles.html", dicc)

def pedido_lista(request, rol):

    dicc = {}
    servicio_pedidos = Client('https://localhost:44344/WebService_pedido.asmx?wsdl')
    servicio_estado = Client('https://localhost:44344/Web_service_estados.asmx?wsdl')
    servicio_productocantidadcarrito = Client('https://localhost:44344/WebService_ProductoCantidadCarrito.asmx?wsdl')

    estados = ""
    if rol == "bodeguero" and "usuario_bodeguero" in request.session:
        estados = "(1)"
    elif rol == "despachador" and "usuario_despachador" in request.session:
        estados = "(2,3)"
    else:
        return redirect("/")



    pedidos = servicio_pedidos.service.MostrarPedidosEstado(estados)
    if (pedidos):
        print(pedidos)

        dicc['pedidos'] = pedidos[0]

        lista_pedidos = []
        
        for p in pedidos[0]:
            tupla = (p, GetCLP(servicio_productocantidadcarrito.service.ValorTotal(p.Id_carrito)))
            lista_pedidos.append(tupla)
        dicc['lista_pedidos'] = lista_pedidos
        print(lista_pedidos)

    return render(request, "main/pedido_lista.html", dicc)

def pedido_estado(request, id_pedido, id_estado):

    servicio_pedidos = Client('https://localhost:44344/WebService_pedido.asmx?wsdl')
    pedido = servicio_pedidos.service.CambiarEstado(id_pedido, id_estado)

    return redirect("/pedidos/"+str(id_pedido)+"/")

def pagar(request, id_pedido):

    servicio_pedidos = Client('https://localhost:44344/WebService_pedido.asmx?wsdl')
    #pedido = servicio_pedidos.service.Pagado(id_pedido)

    pago = servicio_pedidos.service.CrearPago(id_pedido, "http://127.0.0.1:8000/pago_completado/")

    dicc = {}
    dicc["pago"] = pago

    return render(request, "main/pagar.html", dicc)
    #return render(request, "main/confirmacion_compra.html")

@csrf_exempt
def pago_completado(request):
    if request.method == "POST":
        post = request.POST
        token = post["token_ws"]
        print(token)
        servicio_pedido = Client("https://localhost:44344/WebService_pedido.asmx?wsdl")
        if servicio_pedido.service.RevisarPagoToken(token):
            return render(request, "main/confirmacion_compra.html")
    
    return render(request, "main/pago_fallido.html")

def test(request):
    url = 'https://localhost:44344/WebServiceTest1.asmx?wsdl'
    client = Client(url)

    result = client.service.Suma(1, 2)
    print(result)

    result = client.service.HelloWorld()
    print(result)
    return HttpResponse(result)

def GetCLP(price):
    grupos_centenas = []
    string = str(int(price))

    #separar el numero en grupos
    while len(string) > 3:
        grupos_centenas.append(string[-3:])
        string = string[0:-3]
    grupos_centenas.append(string)

    #unir los grupos con puntos
    numero_unido = ""
    for centenar in reversed(grupos_centenas):
        numero_unido += centenar + "."

    numero_unido = numero_unido[0:-1]   

    #agregar contenido extra
    money_clp = "$ "+ numero_unido + " CLP"

    return money_clp

def EmailHide(email):
    lista = email.split("@")
    
    email = lista[0][0:int(len(lista[0])/2)]
    email += "****@"
    email += lista[1]

    return email