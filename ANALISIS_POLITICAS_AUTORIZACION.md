# Análisis de Políticas de Autorización - AutoAlert Backend

## 📋 Políticas Definidas en Program.cs

Tu sistema tiene **17 políticas de autorización** configuradas:

### Gestión de Usuarios
- `VIEW_USERS` - Ver usuarios
- `EDIT_USERS` - Editar usuarios
- `DELETE_USERS` - Eliminar usuarios
- `CREATE_USERS` - Crear usuarios

### Gestión de Permisos
- `UPDATE_PERMISSIONS` - Actualizar permisos

### Gestión de Tiendas
- `VIEW_STORES` - Ver tiendas
- `CREATE_STORES` - Crear tiendas
- `EDIT_STORES` - Editar tiendas
- `DELETE_STORES` - Eliminar tiendas

### Gestión de Servicios
- `VIEW_SERVICES` - Ver servicios
- `CREATE_SERVICES` - Crear servicios
- `EDIT_SERVICES` - Editar servicios
- `DELETE_SERVICES` - Eliminar servicios

### Gestión de Empresas
- `VIEW_COMPANIES` - Ver empresas
- `CREATE_COMPANIES` - Crear empresas
- `EDIT_COMPANIES` - Editar empresas
- `DELETE_COMPANIES` - Eliminar empresas

---

## 🔍 Estado Actual de Implementación

### ✅ Controladores CON Políticas Implementadas

#### UsersController
- ✅ `GetAllUsers` → `VIEW_USERS` (implementado)
- ⚠️ Otros endpoints solo tienen `[Authorize]` genérico

### ⚠️ Controladores CON Autorización Básica (solo `[Authorize]`)
Estos controladores tienen autenticación pero NO políticas específicas:
- CompaniesController
- StoresController
- ServicesController
- RolesController
- AlertsController
- GroupsController
- DocumentTypesController
- ModulesController
- SubModulesController
- RoleSubModulesController
- UserSubmodulesController
- NotificationsController
- LogsController
- UserCompaniesController
- UserGroupsController

### ❌ Controladores SIN Autorización
- AuthController (correcto, es público para login)

---

## 🎯 Recomendaciones de Implementación por Controlador

### 1️⃣ UsersController
**Estado**: Parcialmente implementado

```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // ✅ Ya implementado
    [Authorize(Policy = "VIEW_USERS")]
    [HttpGet("GetAllUsers")]
    public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
    
    // ⚠️ AGREGAR POLÍTICA
    [Authorize(Policy = "VIEW_USERS")]
    [HttpGet("GetUserById/{id}")]
    public async Task<ActionResult<Users>> GetUser(Guid id)
    
    // ⚠️ AGREGAR POLÍTICA
    [Authorize(Policy = "VIEW_USERS")]
    [HttpGet("GetUserByEmail/{email}")]
    public async Task<ActionResult<Users>> GetUserByEmail(string email)
    
    // ⚠️ AGREGAR POLÍTICA
    [Authorize(Policy = "CREATE_USERS")]
    [HttpPost("CreateUser")]
    public async Task<ActionResult<Users>> CreateUser(CreateUserDto newUser)
    
    // ⚠️ AGREGAR POLÍTICA
    [Authorize(Policy = "EDIT_USERS")]
    [HttpPut("UpdateUser/{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, Users user)
    
    // ⚠️ AGREGAR POLÍTICA
    [Authorize(Policy = "DELETE_USERS")]
    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
}
```

---

### 2️⃣ CompaniesController
**Estado**: Solo `[Authorize]` genérico

```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    // AGREGAR POLÍTICAS:
    
    [Authorize(Policy = "VIEW_COMPANIES")]
    [HttpGet("GetAllCompanies")]
    public async Task<ActionResult<IEnumerable<Companies>>> GetAll()
    
    [Authorize(Policy = "VIEW_COMPANIES")]
    [HttpGet("GetCompanyById/{id}")]
    public async Task<ActionResult<Companies>> Get(Guid id)
    
    [Authorize(Policy = "CREATE_COMPANIES")]
    [HttpPost("CreateCompany")]
    public async Task<ActionResult<Companies>> Create(Companies company)
    
    [Authorize(Policy = "EDIT_COMPANIES")]
    [HttpPut("UpdateCompany/{id}")]
    public async Task<IActionResult> Update(Guid id, Companies company)
    
    [Authorize(Policy = "DELETE_COMPANIES")]
    [HttpDelete("DeleteCompany/{id}")]
    public async Task<IActionResult> Delete(Guid id)
}
```

---

### 3️⃣ StoresController
**Estado**: Solo `[Authorize]` genérico

```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    // AGREGAR POLÍTICAS:
    
    [Authorize(Policy = "VIEW_STORES")]
    [HttpGet("GetAllStores")]
    public async Task<ActionResult<IEnumerable<Stores>>> GetAll()
    
    [Authorize(Policy = "VIEW_STORES")]
    [HttpGet("GetStoreById/{id}")]
    public async Task<ActionResult<Stores>> Get(Guid id)
    
    [Authorize(Policy = "CREATE_STORES")]
    [HttpPost("CreateStore")]
    public async Task<ActionResult<Stores>> Create(Stores store)
    
    [Authorize(Policy = "EDIT_STORES")]
    [HttpPut("UpdateStore/{id}")]
    public async Task<IActionResult> Update(Guid id, Stores store)
    
    [Authorize(Policy = "DELETE_STORES")]
    [HttpDelete("DeleteStore/{id}")]
    public async Task<IActionResult> Delete(Guid id)
}
```

---

### 4️⃣ ServicesController
**Estado**: Solo `[Authorize]` genérico

```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    // AGREGAR POLÍTICAS:
    
    [Authorize(Policy = "VIEW_SERVICES")]
    [HttpGet("GetAllServices")]
    public async Task<ActionResult<IEnumerable<Services>>> GetAll()
    
    [Authorize(Policy = "VIEW_SERVICES")]
    [HttpGet("GetServiceById/{id}")]
    public async Task<ActionResult<Services>> Get(Guid id)
    
    [Authorize(Policy = "VIEW_SERVICES")]
    [HttpGet("GetServicesByStoreId/{storeId}")]
    public async Task<ActionResult<IEnumerable<Services>>> GetByStoreId(Guid storeId)
    
    [Authorize(Policy = "CREATE_SERVICES")]
    [HttpPost("CreateService")]
    public async Task<ActionResult<Services>> Create(Services service)
    
    [Authorize(Policy = "EDIT_SERVICES")]
    [HttpPut("UpdateService/{id}")]
    public async Task<IActionResult> Update(Guid id, Services service)
    
    [Authorize(Policy = "DELETE_SERVICES")]
    [HttpDelete("DeleteService/{id}")]
    public async Task<IActionResult> Delete(Guid id)
}
```

---

### 5️⃣ RolesController, AlertsController, GroupsController
**Estado**: Solo `[Authorize]` genérico

**⚠️ PROBLEMA**: No hay políticas definidas en Program.cs para estos recursos.

**Recomendación**: Agregar nuevas políticas en Program.cs:

```csharp
// En Program.cs, agregar a la lista de permissions:
var permissions = new[]
{
    // ... políticas existentes ...
    
    // Roles
    "VIEW_ROLES", "CREATE_ROLES", "EDIT_ROLES", "DELETE_ROLES",
    
    // Alertas
    "VIEW_ALERTS", "CREATE_ALERTS", "EDIT_ALERTS", "DELETE_ALERTS",
    
    // Grupos
    "VIEW_GROUPS", "CREATE_GROUPS", "EDIT_GROUPS", "DELETE_GROUPS",
    
    // Módulos
    "VIEW_MODULES", "CREATE_MODULES", "EDIT_MODULES", "DELETE_MODULES",
    
    // SubMódulos
    "VIEW_SUBMODULES", "CREATE_SUBMODULES", "EDIT_SUBMODULES", "DELETE_SUBMODULES",
    
    // Notificaciones
    "VIEW_NOTIFICATIONS", "CREATE_NOTIFICATIONS", "EDIT_NOTIFICATIONS", "DELETE_NOTIFICATIONS",
    
    // Logs (solo lectura)
    "VIEW_LOGS",
    
    // Tipos de Documento
    "VIEW_DOCUMENT_TYPES", "CREATE_DOCUMENT_TYPES", "EDIT_DOCUMENT_TYPES", "DELETE_DOCUMENT_TYPES"
};
```

---

## 📊 Resumen de Implementación Necesaria

| Controlador | Políticas Necesarias | Estado Actual |
|------------|---------------------|---------------|
| UsersController | VIEW/CREATE/EDIT/DELETE_USERS | ⚠️ Parcial (solo VIEW) |
| CompaniesController | VIEW/CREATE/EDIT/DELETE_COMPANIES | ✅ Definidas, falta implementar |
| StoresController | VIEW/CREATE/EDIT/DELETE_STORES | ✅ Definidas, falta implementar |
| ServicesController | VIEW/CREATE/EDIT/DELETE_SERVICES | ✅ Definidas, falta implementar |
| RolesController | VIEW/CREATE/EDIT/DELETE_ROLES | ❌ No definidas |
| AlertsController | VIEW/CREATE/EDIT/DELETE_ALERTS | ❌ No definidas |
| GroupsController | VIEW/CREATE/EDIT/DELETE_GROUPS | ❌ No definidas |
| ModulesController | VIEW/CREATE/EDIT/DELETE_MODULES | ❌ No definidas |
| SubModulesController | VIEW/CREATE/EDIT/DELETE_SUBMODULES | ❌ No definidas |
| NotificationsController | VIEW/CREATE/EDIT/DELETE_NOTIFICATIONS | ❌ No definidas |
| LogsController | VIEW_LOGS | ❌ No definidas |
| DocumentTypesController | VIEW/CREATE/EDIT/DELETE_DOCUMENT_TYPES | ❌ No definidas |

---

## 🚀 Plan de Acción Recomendado

### Fase 1: Completar Políticas Existentes
1. ✅ Implementar políticas en UsersController (falta CREATE, EDIT, DELETE)
2. ✅ Implementar políticas en CompaniesController
3. ✅ Implementar políticas en StoresController
4. ✅ Implementar políticas en ServicesController

### Fase 2: Definir Nuevas Políticas
1. Agregar políticas para Roles, Alerts, Groups, etc. en Program.cs
2. Implementar en los controladores correspondientes

### Fase 3: Casos Especiales
- **UPDATE_PERMISSIONS**: Implementar en RoleSubModulesController y UserSubmodulesController
- **LogsController**: Solo lectura (VIEW_LOGS)
- **AuthController**: Mantener sin autorización (público)

---

## 💡 Notas Importantes

1. **Doble `[Authorize]`**: En UsersController tienes `[Authorize]` a nivel de clase Y en el método. Puedes remover el del método si ya está en la clase.

2. **PermissionHandler**: Tu sistema usa un handler personalizado que valida permisos contra la base de datos mediante `IPermissionService.HasPermissionAsync()`.

3. **Jerarquía de permisos**: Las políticas se evalúan por usuario individual, considerando:
   - Permisos del rol del usuario (RoleSubModules)
   - Permisos personalizados del usuario (UserSubmodules)

4. **Seguridad**: Actualmente muchos endpoints están protegidos solo con `[Authorize]`, lo que significa que cualquier usuario autenticado puede acceder. Implementar las políticas específicas mejorará significativamente la seguridad.

---

## 🔧 Siguiente Paso

¿Quieres que implemente las políticas en los controladores? Puedo:
1. Completar UsersController
2. Implementar en Companies, Stores y Services
3. Agregar las nuevas políticas en Program.cs
4. Implementar en todos los controladores restantes
