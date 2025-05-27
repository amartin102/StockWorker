# StockWorker - Contenedor Docker para Aplicación .NET

Este proyecto contiene los pasos para compilar y ejecutar una aplicación .NET en un contenedor Docker basado en Linux.

## 🚀 Pasos para publicar y ejecutar

### 1. Construir la imagen Docker

Una vez publicada la aplicación, crea la imagen Docker con el siguiente comando:

```bash
docker build -t stockworker-image .
```

Este comando crea una imagen Docker llamada `stockworker-image` a partir del `Dockerfile` ubicado en el directorio raíz del proyecto.

---

### 2. Ejecutar el contenedor

Finalmente, ejecuta el contenedor Docker con el siguiente comando:

```bash
docker run -d -p 8081:8081 --name stockworker-container stockworker-image
```

Este comando:

- Ejecuta el contenedor en segundo plano (`-d`)
- Expone el puerto 81 dentro del contenedor como el puerto 8081 en tu máquina
- Le asigna el nombre `stockworker-container`

---

## 📁 Estructura esperada

```
/StockWorkerSn/
├── publish/
│   └── (archivos publicados con dotnet publish)
├── Dockerfile
├── README.md
```

---

## 🧰 Requisitos

- [.NET SDK 6 o superior](https://dotnet.microsoft.com/)
- [Docker](https://www.docker.com/)

---

## ✅ Notas

- Asegúrate de tener Docker configurado en modo contenedores Linux para evitar errores de compatibilidad.
- Si ya tienes contenedores en modo Windows, tendrás que detenerlos al cambiar al modo Linux, ya que Docker Desktop no ejecuta ambos al mismo tiempo.