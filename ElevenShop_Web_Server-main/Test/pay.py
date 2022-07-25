from suds.client import Client

x = Client("http://localhost:57818/WebService/WebService1.asmx?wsdl")

pago = x.service.CrearPago()

print(pago)
