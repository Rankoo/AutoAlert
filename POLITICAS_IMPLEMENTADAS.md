# Políticas de Autorización Implementadas - AutoAlert

## 📋 Resumen de Políticas en Program.cs

Se han definido **61 políticas de autorización** organizadas por módulos funcionales:

---

## 🔐 Políticas por Módulo

### 👥 Gestión de Usuarios (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_USERS` | Ver usuarios | GET endpoints |
| `CREATE_USERS` | Crear usuarios | POST endpoints |
| `EDIT_USERS` | Editar usuarios | PUT endpoints |
| `DELETE_USERS` | Eliminar usuarios | DELETE endpoints |

**Controlador**: `UsersController` ✅ Implementado

---

### 🎭 Gestión de Roles (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_ROLES` | Ver roles | GET endpoints |
| `CREATE_ROLES` | Crear roles | POST endpoints |
| `EDIT_ROLES` | Editar roles | PUT endpoints |
| `DELETE_ROLES` | Eliminar roles | DELETE endpoints |

**Controlador**: `RolesController` ⏳ Pendiente de implementar

---

### 🏢 Gestión de Empresas (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_COMPANIES` | Ver empresas | GET endpoints |
| `CREATE_COMPANIES` | Crear empresas | POST endpoints |
| `EDIT_COMPANIES` | Editar empresas | PUT endpoints |
| `DELETE_COMPANIES` | Eliminar empresas | DELETE endpoints |

**Controlador**: `CompaniesController` ✅ Implementado

---

### 👥 Gestión de Grupos (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_GROUPS` | Ver grupos | GET endpoints |
| `CREATE_GROUPS` | Crear grupos | POST endpoints |
| `EDIT_GROUPS` | Editar grupos | PUT endpoints |
| `DELETE_GROUPS` | Eliminar grupos | DELETE endpoints |

**Controlador**: `GroupsController` ⏳ Pendiente de implementar

---

### 🏪 Gestión de Tiendas (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_STORES` | Ver tiendas | GET endpoints |
| `CREATE_STORES` | Crear tiendas | POST endpoints |
| `EDIT_STORES` | Editar tiendas | PUT endpoints |
| `DELETE_STORES` | Eliminar tiendas | DELETE endpoints |

**Controlador**: `StoresController` ✅ Implementado

---

### ⚡ Gestión de Servicios (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_SERVICES` | Ver servicios públicos | GET endpoints |
| `CREATE_SERVICES` | Crear servicios | POST endpoints |
| `EDIT_SERVICES` | Editar servicios | PUT endpoints |
| `DELETE_SERVICES` | Eliminar servicios | DELETE endpoints |

**Controlador**: `ServicesController` ✅ Implementado

---

### 🚨 Gestión de Alertas (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_ALERTS` | Ver alertas | GET endpoints |
| `CREATE_ALERTS` | Crear alertas | POST endpoints |
| `EDIT_ALERTS` | Editar alertas | PUT endpoints |
| `DELETE_ALERTS` | Eliminar alertas | DELETE endpoints |

**Controlador**: `AlertsController` ⏳ Pendiente de implementar

---

### 🔔 Gestión de Notificaciones (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_NOTIFICATIONS` | Ver notificaciones | GET endpoints |
| `CREATE_NOTIFICATIONS` | Crear notificaciones | POST endpoints |
| `EDIT_NOTIFICATIONS` | Editar notificaciones | PUT endpoints |
| `DELETE_NOTIFICATIONS` | Eliminar notificaciones | DELETE endpoints |

**Controlador**: `NotificationsController` ⏳ Pendiente de implementar

---

### 📦 Gestión de Módulos (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_MODULES` | Ver módulos | GET endpoints |
| `CREATE_MODULES` | Crear módulos | POST endpoints |
| `EDIT_MODULES` | Editar módulos | PUT endpoints |
| `DELETE_MODULES` | Eliminar módulos | DELETE endpoints |

**Controlador**: `ModulesController` ⏳ Pendiente de implementar

---

### 📋 Gestión de SubMódulos (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_SUBMODULES` | Ver submódulos | GET endpoints |
| `CREATE_SUBMODULES` | Crear submódulos | POST endpoints |
| `EDIT_SUBMODULES` | Editar submódulos | PUT endpoints |
| `DELETE_SUBMODULES` | Eliminar submódulos | DELETE endpoints |

**Controlador**: `SubModulesController` ⏳ Pendiente de implementar

---

### 📄 Gestión de Tipos de Documento (4 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_DOCUMENT_TYPES` | Ver tipos de documento | GET endpoints |
| `CREATE_DOCUMENT_TYPES` | Crear tipos de documento | POST endpoints |
| `EDIT_DOCUMENT_TYPES` | Editar tipos de documento | PUT endpoints |
| `DELETE_DOCUMENT_TYPES` | Eliminar tipos de documento | DELETE endpoints |

**Controlador**: `DocumentTypesController` ⏳ Pendiente de implementar

---

### 🔑 Gestión de Permisos (2 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_PERMISSIONS` | Ver permisos | GET endpoints |
| `UPDATE_PERMISSIONS` | Actualizar permisos | POST/PUT/DELETE endpoints |

**Controladores**: 
- `RoleSubModulesController` ⏳ Pendiente de implementar
- `UserSubmodulesController` ⏳ Pendiente de implementar

---

### 🏢👤 Gestión de Relaciones Usuario-Empresa (3 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_USER_COMPANIES` | Ver relaciones usuario-empresa | GET endpoints |
| `CREATE_USER_COMPANIES` | Asignar empresas a usuarios | POST endpoints |
| `DELETE_USER_COMPANIES` | Remover empresas de usuarios | DELETE endpoints |

**Controlador**: `UserCompaniesController` ⏳ Pendiente de implementar

---

### 👥👤 Gestión de Relaciones Usuario-Grupo (3 políticas)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_USER_GROUPS` | Ver relaciones usuario-grupo | GET endpoints |
| `CREATE_USER_GROUPS` | Asignar grupos a usuarios | POST endpoints |
| `DELETE_USER_GROUPS` | Remover grupos de usuarios | DELETE endpoints |

**Controlador**: `UserGroupsController` ⏳ Pendiente de implementar

---

### 📊 Auditoría y Logs (1 política)
| Política | Descripción | Uso |
|----------|-------------|-----|
| `VIEW_LOGS` | Ver logs del sistema | GET endpoints |

**Controlador**: `LogsController` ⏳ Pendiente de implementar

**Nota**: Los logs son solo de lectura por seguridad. No se permiten operaciones de creación, edición o eliminación.

---

## 📊 Estado de Implementación

### ✅ Completados (4 controladores)
1. UsersController
2. CompaniesController
3. StoresController
4. ServicesController

### ⏳ Pendientes (13 controladores)
1. RolesController
2. GroupsController
3. AlertsController
4. NotificationsController
5. ModulesController
6. SubModulesController
7. DocumentTypesController
8. RoleSubModulesController
9. UserSubmodulesController
10. UserCompaniesController
11. UserGroupsController
12. LogsController
13. AuthController (sin políticas - público)

---

## 🔒 Cómo Funcionan las Políticas

### Flujo de Autorización

1. **Usuario hace request** → Incluye JWT token en cookie
2. **Middleware de autenticación** → Valida el token JWT
3. **Middleware de autorización** → Verifica la política del endpoint
4. **PermissionHandler** → Consulta permisos en base de datos
5. **IPermissionService** → Valida si el usuario tiene el permiso
6. **Respuesta** → 200 OK o 403 Forbidden

### Ejemplo de Validación

```csharp
// Endpoint protegido
[Authorize(Policy = "VIEW_USERS")]
[HttpGet("GetAllUsers")]
public async Task<ActionResult<IEnumerable<Users>>> GetUsers()

// El PermissionHandler verifica:
// 1. ¿El usuario está autenticado? (JWT válido)
// 2. ¿El usuario tiene el permiso "VIEW_USERS"?
//    - Verifica en RoleSubModules (permisos del rol)
//    - Verifica en UserSubmodules (permisos personalizados)
// 3. Si tiene permiso → Permite acceso
//    Si no tiene permiso → 403 Forbidden
```

---

## 🎯 Próximos Pasos

1. ✅ Definir políticas en Program.cs (COMPLETADO)
2. ⏳ Implementar políticas en los 13 controladores restantes
3. ⏳ Configurar permisos en la base de datos
4. ⏳ Asignar permisos a roles
5. ⏳ Probar el sistema de autorización

---

## 💡 Recomendaciones de Seguridad

### Permisos por Rol Sugeridos

#### Administrador
- Todos los permisos (VIEW, CREATE, EDIT, DELETE) en todos los módulos

#### Supervisor
- VIEW en todos los módulos
- CREATE, EDIT, DELETE en: Stores, Services, Alerts, Notifications

#### Usuario Estándar
- VIEW en: Companies, Stores, Services, Alerts, Notifications
- CREATE en: Alerts, Notifications (solo para sus tiendas)

#### Auditor
- VIEW_LOGS
- VIEW en todos los módulos (solo lectura)

---

## 🔧 Configuración en Base de Datos

Para que las políticas funcionen, debes:

1. **Crear SubMódulos** con los nombres exactos de las políticas:
   ```sql
   INSERT INTO SubModules (Name, ...) VALUES 
   ('VIEW_USERS', ...),
   ('CREATE_USERS', ...),
   -- etc.
   ```

2. **Asignar permisos a Roles** en RoleSubModules:
   ```sql
   INSERT INTO RoleSubModules (RoleId, SubModuleId, CanView, CanCreate, CanEdit, CanDelete)
   VALUES (@AdminRoleId, @ViewUsersSubModuleId, 1, 1, 1, 1)
   ```

3. **Permisos personalizados** (opcional) en UserSubmodules:
   ```sql
   INSERT INTO UserSubmodules (UserId, SubModuleId, CanView, CanCreate, CanEdit, CanDelete)
   VALUES (@UserId, @SubModuleId, 1, 0, 0, 0)
   ```

---

## 📝 Notas Importantes

- **AuthController**: No requiere políticas (endpoints públicos de login)
- **Logs**: Solo lectura por seguridad y auditoría
- **Relaciones Usuario-Empresa/Grupo**: No tienen EDIT porque se crean/eliminan directamente
- **UPDATE_PERMISSIONS**: Política especial para gestionar permisos de otros usuarios
