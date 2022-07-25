"""ElevenShop_Web_Server URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/2.2/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path
from main import views as main_views

urlpatterns = [
    #path('admin/', admin.site.urls),
    path('', main_views.home, name='home'),

    #carrito
    path('carrito/', main_views.carrito, name="carrito_compras"),
    path('carrito/agregar/<int:id_producto>/', main_views.carrito_add_prod, name="carrito_compras_add"),
    path('carrito/remover/<int:id_producto>/', main_views.carrito_remove_prod, name="carrito_compras_remove"),
    path('carrito/eliminar/<int:id_producto>/', main_views.carrito_delete_prod, name="carrito_compras_delete"),
    path('carrito/delete/', main_views.carrito_delete, name="carrito_delete"),
    path('reservar/', main_views.carrito_reservar, name="reservar"),

    #productos
    path('producto/<str:id_producto>/', main_views.producto, name="producto"),
    path('producto_mantenedor/', main_views.producto_lista, name="producto_lista"),
    path('producto_mantenedor/<str:idproducto>/', main_views.producto_detalle, name="producto_detalle"),
    
    #pedidos y pagos
    path('pedidos/', main_views.pedido_usuario, name="pedidos"),
    path('pagar/<str:id_pedido>/', main_views.pagar, name="pagar"),
    path('pago_completado/', main_views.pago_completado, name="pago_completado"),
    path('pedidos/<str:id_pedido>/', main_views.pedido_detalle, name="pedido_detalle"),
    path('pedidos_mantenedor_lista/<str:rol>/', main_views.pedido_lista, name="pedido_lista"),
    path('pedido_estado/<int:id_pedido>/<int:id_estado>/', main_views.pedido_estado, name="pedido_estado"),

    #sesion
    path('sesion/', main_views.inicio_sesion, name="inicio_sesion"),
    path('registrarse/', main_views.registrarse, name="registrarse"),
    path('logout/', main_views.cerrar_sesion, name="cerrar_sesion"),

    #cambiar pass
    path('pedir-nueva-clave/', main_views.cambiar_clave_pedir, name="CambiarClave"),
    path('cambiar-clave/', main_views.cambiar_clave, name="CambiarClaveFin"),
    

    #administracion
    path('usuario-lista/<str:tipo>/', main_views.usuario_lista, name="usuario-lista"),
    path('usuario/<str:rut>/', main_views.usuario_perfil, name="usuario-detalle"),



    #otros
    path('test/', main_views.test, name="test"),
]
