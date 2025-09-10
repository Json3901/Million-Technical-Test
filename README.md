# Million Technical Test

Este proyecto es un portal inmobiliario desarrollado como parte de la prueba técnica de Million. Incluye:

- **BackEnd/**: API REST en C# (.NET) para la gestión de propiedades.
- **FrontEnd/real-estate-portal/**: Aplicación web en React (Next.js).
- **data/properties.json**: Datos de ejemplo para inicializar MongoDB.
- **docker-compose.yml**: Orquestación de servicios.

## Requisitos Previos

- [Docker](https://www.docker.com/) y [Docker Compose](https://docs.docker.com/compose/) instalados.
- (Opcional para ejecución independiente) [.NET 7+](https://dotnet.microsoft.com/) y [Node.js 18+](https://nodejs.org/).

---

## Ejecución con Docker Compose

1. Clona el repositorio y navega a la raíz del proyecto.
2. Ejecuta:

   ```bash
   docker-compose up --build

# Real Estate Portal 🏡

## Accede a los servicios
- **Frontend**: [http://localhost:3000](http://localhost:3000)
- **Backend (Swagger)**: [http://localhost:5050/swagger](http://localhost:5050/swagger)
- **MongoDB**: Puerto `27017`

Los servicios se levantarán y los datos de ejemplo se cargarán automáticamente en **MongoDB**.

---

## Ejecución alternativa desde IDEs (VSCode, Rider, Visual Studio)

### 1. Levantar MongoDB
Puedes usar Docker para iniciar la base de datos con los datos de ejemplo:

```bash
docker run -d --name realestateportal-mongo \
  -p 27017:27017 \
  -v $(pwd)/data/properties.json:/docker-entrypoint-initdb.d/properties.json:ro \
  mongo:7.0
```

### 2. Backend (.NET)
1. Abre la carpeta `BackEnd/src/RealEstatePortal.API` en tu IDE.
2. Restaura las dependencias:
   ```bash
   dotnet restore
   ```
3. Ejecuta el proyecto:
   ```bash
   dotnet run
   ```
4. El backend estará disponible en [http://localhost:8080](http://localhost:8080).
5. Verifica que la cadena de conexión apunte a:
   ```
   mongodb://localhost:27017
   ```

### 3. Frontend (Next.js)
1. Abre la carpeta `FrontEnd/real-estate-portal` en tu IDE.
2. Instala las dependencias:
   ```bash
   npm install
   ```
3. Ejecuta el frontend:
   ```bash
   npm run dev
   ```
4. El frontend estará disponible en [http://localhost:3000](http://localhost:3000).
5. Si necesitas cambiar la URL del backend, crea o edita el archivo `.env.local` con la variable:
   ```
   NEXT_PUBLIC_API_URL=http://localhost:8080
   ```

---

## Notas
- El archivo `data/properties.json` inicializa la base de datos con datos de ejemplo.
- Puedes modificar los puertos en `docker-compose.yml` según tus necesidades.
- Puedes abrir ambos proyectos en diferentes ventanas del IDE para trabajar y depurar de forma independiente.
- Asegúrate de que **MongoDB** esté corriendo antes de iniciar el backend.

---

✅ ¡Listo! Levanta los servicios y explora el portal inmobiliario 🚀  
