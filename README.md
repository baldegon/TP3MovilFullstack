# TP3MovilFullstack — MAUI Blazor Hybrid

Catálogo de películas + panel de usuarios, construido con **.NET MAUI Blazor (Blazor Hybrid)**. Maneja los datos en memoria y muestra imágenes locales convirtiéndolas a **Data URL (Base64)** para que el WebView pueda renderizarlas sin drama.

---

## ✅ Características principales

* **Listado de Películas** con tarjetas (título, director, año y póster).
* **Detalle de Película** (con breadcrumb, acciones y modal de confirmación).
* **Usuarios (Master–Detail)**: lista a la izquierda, detalle editable a la derecha.
* **Control por Rol** (admin / user) para mostrar/ocultar acciones.
* **Imágenes locales** (incluyendo `AppDataDirectory` y recursos del paquete) renderizadas con **Base64**.
* Estilo **minimalista** (paleta suave, cards, responsive con grid).

---

## 🧱 Estructura (carpetas relevantes)

```
TP3MovilFullstack/
├─ Models/
│  ├─ Pelicula.cs
│  └─ Usuario.cs
├─ Services/
│  ├─ PeliculaService.cs
│  └─ UserService.cs
├─ Utils/
│  └─ ImageHelper.cs
├─ Pages/ (o Components/Pages/)
│  ├─ Peliculas.razor
│  ├─ PeliculaDetalle.razor (opcional/nombre similar)
│  └─ Usuarios.razor
└─ wwwroot/
   └─ css/site.css
```

---

## 🛠 Requisitos

* **.NET 8 SDK** (recomendado) y **workloads de MAUI**.
* Windows / macOS con **Visual Studio 2022 17.8+** (o CLI de .NET).

Instalar workloads (si hace falta):

```bash
dotnet workload install maui
```

---

## ▶️ Cómo ejecutar

1. Restaurar y compilar:

   ```bash
   dotnet restore
   dotnet build
   ```
2. Ejecutar (ej. Windows):

   ```bash
   dotnet build -t:Run -f net8.0-windows10.0.19041.0
   ```

   O desde Visual Studio, seleccione el destino (Windows/Android/iOS) y **Start**.

> Tip: En Android/iOS, los paths de archivos difieren. El proyecto usa `FileSystem.AppDataDirectory` para almacenar imágenes del usuario.

---

## 🖼 Gestión de imágenes (MAUI + WebView)

Los `<img src="C:\...">` **no funcionan** directo en Blazor Hybrid. Solución:

* `ImageHelper.ToDataUrl(path)` lee el archivo y genera `src="data:image/...;base64,..."`.
* Soporta rutas **absolutas** y relativas a:

  * `FileSystem.AppDataDirectory` (datos del usuario)
  * `FileSystem.AppPackageDirectory` (recursos del paquete)
* Si no existe la imagen, muestra un **fallback SVG**.

### Sembrado de imágenes por defecto

Si trae imágenes en `Resources/Images`, al inicializar puede copiarlas a `AppDataDirectory`:

```csharp
var src = Path.Combine(FileSystem.AppPackageDirectory, "Resources", "Images", "perro.jpg");
var dst = Path.Combine(FileSystem.AppDataDirectory, "perro.jpg");
if (File.Exists(src)) { File.Copy(src, dst, true); }
usuario.ImagenPath = dst;
```

---

## 🧩 Páginas clave

### Películas (`Peliculas.razor`)

* Grilla responsive con tarjetas.
* Póster usando `ImageHelper.ToDataUrl(p.ImagenPath)`.
* Botones **Agregar/Editar/Eliminar** visibles según rol.

### Detalle de Película

* Card con datos, breadcrumb y acciones.
* Modal de confirmación para eliminar.

### Usuarios (`Usuarios.razor`)

* **Master–Detail**: lista a la izquierda, detalle editable a la derecha.
* Avatar con Data URL y fallback.

---

## 🔐 Roles y visibilidad de acciones

Comparación robusta del rol (evita fallas por espacios o mayúsculas):

```csharp
private static bool IsAdmin(Usuario? u) =>
    string.Equals(u?.Rol?.Trim(), "admin", StringComparison.OrdinalIgnoreCase);
```

**Uso en Razor:**

```razor
@if (IsAdmin(CurrentUser))
{
    <button class="btn btn-danger">Eliminar</button>
}
```

> Cambie la condición según su política (solo admin edita/elimina, etc.).

---

## 🧪 Pruebas rápidas / Troubleshooting

* **No se ven imágenes**: verifique que `ImagenPath` apunte a un archivo **existente**. Use `ImageHelper.ToDataUrl(path)`. En Android/iOS, use `AppDataDirectory`.
* **Botones que no cambian según rol**: confirme el rol real (`Trim()` + comparación `OrdinalIgnoreCase`).
* **Null en CurrentUser**: establezca sesión con `UserService.ValidateLogin(...)` o seleccione un usuario por defecto.

---

## 🧭 Roadmap (sugerencias de mejora)

* Autenticación real (no almacenar contraseñas en texto plano).
* Persistencia en BD (SQLite) + repositorios async.
* Subida de imágenes con FilePicker y copia a `AppDataDirectory`.
* Paginación y búsqueda en películas/usuarios.
* Tests unitarios para servicios.

> Seguridad: este proyecto es **educativo**. No use este esquema de contraseñas para producción.

---

## 📝 Créditos

* **.NET MAUI + Blazor**
* **Bootstrap Icons** (clases `bi bi-*`).
