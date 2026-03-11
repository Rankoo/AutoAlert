# Estandarización de Endpoints - AutoAlert API

## 📋 Mejores Prácticas de Diseño de APIs RESTful

### Principios Aplicados:
1. **Nombres en plural**: `/users` en lugar de `/user`
2. **Uso de sustantivos, no verbos**: `/users` en lugar de `/getUsers`
3. **Jerarquía clara**: `/users/{id}` para recursos específicos
4. **Métodos HTTP semánticos**: GET, POST, PUT, DELETE
5. **Rutas limpias y descriptivas**: Sin prefijos redundantes como "Get", "Create", etc.
6. **Consistencia**: Mismo patrón en todos los controladores

---

## 🔄 Cambios por Controlador

### 1. UsersController

#### ❌ Antes (No RESTful)
```
GET    /api/Users/GetAllUsers
GET    /api/Users/GetUserById/{id}
GET    /api/Users/GetUserByEmail/{email}
POST   /api/Users/CreateUser
PUT    /api/Users/UpdateUser/{id}
DELETE /api/Users/DeleteUser/{id}
```

#### ✅ Después (RESTful)
```
GET    /api/users              → Obtener todos los usuarios
GET    /api/users/{id}         → Obtener usuario por ID
GET    /api/users/email/{email} → Obtener usuario por email
POST   /api/users              → Crear usuario
PUT    /api/users/{id}         → Actualizar usuario
DELETE /api/users/{id}         → Eliminar usuario
```

---

### 2. RolesController

#### ❌ Antes
```
GET    /api/Roles/GetAllRoles
GET    /api/Roles/GetRolById/{id}
GET    /api/Roles/GetRolByName/{name}
POST   /api/Roles/CreateRol
PUT    /api/Roles/UpdateRol/{id}
DELETE /api/Roles/DeleteRol/{id}
```

#### ✅ Después
```
GET    /api/roles              → Obtener todos los roles
GET    /api/roles/{id}         → Obtener rol por ID
GET    /api/roles/name/{name}  → Obtener rol por nombre
POST   /api/roles              → Crear rol
PUT    /api/roles/{id}         → Actualizar rol
DELETE /api/roles/{id}         → Eliminar rol
```

---

### 3. CompaniesController

#### ❌ Antes
```
GET    /api/Companies/GetAllCompanies
GET    /api/Companies/GetCompanyById/{id}
POST   /api/Companies/CreateCompany
PUT    /api/Companies/UpdateCompany/{id}
DELETE /api/Companies/DeleteCompany/{id}
```

#### ✅ Después
```
GET    /api/companies          → Obtener todas las empresas
GET    /api/companies/{id}     → Obtener empresa por ID
POST   /api/companies          → Crear empresa
PUT    /api/companies/{id}     → Actualizar empresa
DELETE /api/companies/{id}     → Eliminar empresa
```

---

### 4. GroupsController

#### ❌ Antes
```
GET    /api/Groups/GetAllGroups
GET    /api/Groups/GetGroupById/{id}
POST   /api/Groups/CreateGroup
PUT    /api/Groups/UpdateGroup/{id}
DELETE /api/Groups/DeleteGroup/{id}
```

#### ✅ Después
```
GET    /api/groups             → Obtener todos los grupos
GET    /api/groups/{id}        → Obtener grupo por ID
POST   /api/groups             → Crear grupo
PUT    /api/groups/{id}        → Actualizar grupo
DELETE /api/groups/{id}        → Eliminar grupo
```

---

### 5. StoresController

#### ❌ Antes
```
GET    /api/Stores/GetAllStores
GET    /api/Stores/GetStoreById/{id}
POST   /api/Stores/CreateStore
PUT    /api/Stores/UpdateStore/{id}
DELETE /api/Stores/DeleteStore/{id}
```

#### ✅ Después
```
GET    /api/stores             → Obtener todas las tiendas
GET    /api/stores/{id}        → Obtener tienda por ID
POST   /api/stores             → Crear tienda
PUT    /api/stores/{id}        → Actualizar tienda
DELETE /api/stores/{id}        → Eliminar tienda
```

---

### 6. ServicesController

#### ❌ Antes
```
GET    /api/Services/GetAllServices
GET    /api/Services/GetServiceById/{id}
GET    /api/Services/GetServicesByStoreId/{storeId}
POST   /api/Services/CreateService
PUT    /api/Services/UpdateService/{id}
DELETE /api/Services/DeleteService/{id}
```

#### ✅ Después
```
GET    /api/services                    → Obtener todos los servicios
GET    /api/services/{id}               → Obtener servicio por ID
GET    /api/stores/{storeId}/services   → Obtener servicios de una tienda
POST   /api/services                    → Crear servicio
PUT    /api/services/{id}               → Actualizar servicio
DELETE /api/services/{id}               → Eliminar servicio
```

---

### 7. AlertsController

#### ❌ Antes
```
GET    /api/Alerts/GetAllAlerts
GET    /api/Alerts/GetAlertById/{id}
GET    /api/Alerts/GetAlertsByServiceId/{serviceId}
GET    /api/Alerts/GetScheduledAlerts
POST   /api/Alerts/CreateAlert
PUT    /api/Alerts/UpdateAlert/{id}
DELETE /api/Alerts/DeleteAlert/{id}
```

#### ✅ Después
```
GET    /api/alerts                      → Obtener todas las alertas
GET    /api/alerts/{id}                 → Obtener alerta por ID
GET    /api/services/{serviceId}/alerts → Obtener alertas de un servicio
GET    /api/alerts/scheduled            → Obtener alertas programadas
POST   /api/alerts                      → Crear alerta
PUT    /api/alerts/{id}                 → Actualizar alerta
DELETE /api/alerts/{id}                 → Eliminar alerta
```

---

### 8. NotificationsController

#### ❌ Antes
```
GET    /api/Notifications/GetAllNotifications
GET    /api/Notifications/GetNotificationById/{id}
GET    /api/Notifications/GetNotificationsByAlertId/{alertId}
GET    /api/Notifications/GetNotificationsByUserId/{userId}
POST   /api/Notifications/CreateNotification
PUT    /api/Notifications/UpdateNotification/{id}
DELETE /api/Notifications/DeleteNotification/{id}
```

#### ✅ Después
```
GET    /api/notifications                  → Obtener todas las notificaciones
GET    /api/notifications/{id}             → Obtener notificación por ID
GET    /api/alerts/{alertId}/notifications → Obtener notificaciones de una alerta
GET    /api/users/{userId}/notifications   → Obtener notificaciones de un usuario
POST   /api/notifications                  → Crear notificación
PUT    /api/notifications/{id}             → Actualizar notificación
DELETE /api/notifications/{id}             → Eliminar notificación
```

---

### 9. ModulesController

#### ❌ Antes
```
GET    /api/Modules/GetAllModules
GET    /api/Modules/GetModuleById/{id}
POST   /api/Modules/CreateModule
PUT    /api/Modules/UpdateModule/{id}
DELETE /api/Modules/DeleteModule/{id}
```

#### ✅ Después
```
GET    /api/modules            → Obtener todos los módulos
GET    /api/modules/{id}       → Obtener módulo por ID
POST   /api/modules            → Crear módulo
PUT    /api/modules/{id}       → Actualizar módulo
DELETE /api/modules/{id}       → Eliminar módulo
```

---

### 10. SubModulesController

#### ❌ Antes
```
GET    /api/SubModules/GetAllSubModules
GET    /api/SubModules/GetSubModuleById/{id}
GET    /api/SubModules/GetSubModulesByModuleId/{moduleId}
POST   /api/SubModules/CreateSubModule
PUT    /api/SubModules/UpdateSubModule/{id}
DELETE /api/SubModules/DeleteSubModule/{id}
```

#### ✅ Después
```
GET    /api/submodules                   → Obtener todos los submódulos
GET    /api/submodules/{id}              → Obtener submódulo por ID
GET    /api/modules/{moduleId}/submodules → Obtener submódulos de un módulo
POST   /api/submodules                   → Crear submódulo
PUT    /api/submodules/{id}              → Actualizar submódulo
DELETE /api/submodules/{id}              → Eliminar submódulo
```

---

### 11. DocumentTypesController

#### ❌ Antes
```
GET    /api/DocumentTypes/GetAllDocumentTypes
GET    /api/DocumentTypes/GetDocumentTypeById/{id}
POST   /api/DocumentTypes/CreateDocumentType
PUT    /api/DocumentTypes/UpdateDocumentType/{id}
DELETE /api/DocumentTypes/DeleteDocumentType/{id}
```

#### ✅ Después
```
GET    /api/document-types     → Obtener todos los tipos de documento
GET    /api/document-types/{id} → Obtener tipo de documento por ID
POST   /api/document-types     → Crear tipo de documento
PUT    /api/document-types/{id} → Actualizar tipo de documento
DELETE /api/document-types/{id} → Eliminar tipo de documento
```

---

### 12. RoleSubModulesController

#### ❌ Antes
```
GET    /api/RoleSubModules/GetAllRoleSubModules
GET    /api/RoleSubModules/GetRoleSubModuleById/{id}
GET    /api/RoleSubModules/GetRoleSubModulesByRoleId/{roleId}
GET    /api/RoleSubModules/GetRoleSubModulesBySubModuleId/{subModuleId}
POST   /api/RoleSubModules/CreateRoleSubModule
PUT    /api/RoleSubModules/UpdateRoleSubModule/{id}
DELETE /api/RoleSubModules/DeleteRoleSubModule/{id}
```

#### ✅ Después
```
GET    /api/permissions/roles                    → Obtener todos los permisos de roles
GET    /api/permissions/roles/{id}               → Obtener permiso por ID
GET    /api/roles/{roleId}/permissions           → Obtener permisos de un rol
GET    /api/submodules/{subModuleId}/role-permissions → Permisos por submódulo
POST   /api/permissions/roles                    → Crear permiso de rol
PUT    /api/permissions/roles/{id}               → Actualizar permiso de rol
DELETE /api/permissions/roles/{id}               → Eliminar permiso de rol
```

---

### 13. UserSubmodulesController

#### ❌ Antes
```
GET    /api/UserSubmodules/GetAllUserSubmodules
GET    /api/UserSubmodules/GetUserSubmoduleById?userId={userId}&subModuleId={subModuleId}
GET    /api/UserSubmodules/GetUserSubmodulesByUserId/{userId}
GET    /api/UserSubmodules/GetUserSubmodulesBySubModuleId/{subModuleId}
POST   /api/UserSubmodules/CreateUserSubmodule
PUT    /api/UserSubmodules/UpdateUserSubmodule?userId={userId}&subModuleId={subModuleId}
DELETE /api/UserSubmodules/DeleteUserSubmodule?userId={userId}&subModuleId={subModuleId}
```

#### ✅ Después
```
GET    /api/permissions/users                    → Obtener todos los permisos de usuarios
GET    /api/permissions/users/{userId}/{subModuleId} → Obtener permiso específico
GET    /api/users/{userId}/permissions           → Obtener permisos de un usuario
GET    /api/submodules/{subModuleId}/user-permissions → Permisos por submódulo
POST   /api/permissions/users                    → Crear permiso de usuario
PUT    /api/permissions/users/{userId}/{subModuleId} → Actualizar permiso
DELETE /api/permissions/users/{userId}/{subModuleId} → Eliminar permiso
```

---

### 14. UserCompaniesController

#### ❌ Antes
```
GET    /api/UserCompanies/GetAllUserCompanies
GET    /api/UserCompanies/GetUserCompanyById?userId={userId}&companyId={companyId}
GET    /api/UserCompanies/GetUserCompaniesByUserId/{userId}
GET    /api/UserCompanies/GetUserCompaniesByCompanyId/{companyId}
POST   /api/UserCompanies/CreateUserCompany
PUT    /api/UserCompanies/UpdateUserCompany?userId={userId}&companyId={companyId}
DELETE /api/UserCompanies/DeleteUserCompany?userId={userId}&companyId={companyId}
```

#### ✅ Después
```
GET    /api/user-companies                       → Obtener todas las relaciones
GET    /api/user-companies/{userId}/{companyId}  → Obtener relación específica
GET    /api/users/{userId}/companies             → Obtener empresas de un usuario
GET    /api/companies/{companyId}/users          → Obtener usuarios de una empresa
POST   /api/user-companies                       → Crear relación
PUT    /api/user-companies/{userId}/{companyId}  → Actualizar relación
DELETE /api/user-companies/{userId}/{companyId}  → Eliminar relación
```

---

### 15. UserGroupsController

#### ❌ Antes
```
GET    /api/UserGroups/GetAllUserGroups
GET    /api/UserGroups/GetUserGroupById?userId={userId}&groupId={groupId}
GET    /api/UserGroups/GetUserGroupsByUserId/{userId}
GET    /api/UserGroups/GetUserGroupsByGroupId/{groupId}
POST   /api/UserGroups/CreateUserGroup
PUT    /api/UserGroups/UpdateUserGroup?userId={userId}&groupId={groupId}
DELETE /api/UserGroups/DeleteUserGroup?userId={userId}&groupId={groupId}
```

#### ✅ Después
```
GET    /api/user-groups                       → Obtener todas las relaciones
GET    /api/user-groups/{userId}/{groupId}    → Obtener relación específica
GET    /api/users/{userId}/groups             → Obtener grupos de un usuario
GET    /api/groups/{groupId}/users            → Obtener usuarios de un grupo
POST   /api/user-groups                       → Crear relación
PUT    /api/user-groups/{userId}/{groupId}    → Actualizar relación
DELETE /api/user-groups/{userId}/{groupId}    → Eliminar relación
```

---

### 16. LogsController

#### ❌ Antes
```
GET    /api/Logs/GetAllLogs
GET    /api/Logs/GetLogById/{id}
GET    /api/Logs/GetLogsByUserId/{userId}
GET    /api/Logs/GetLogsByTableName/{tableName}
GET    /api/Logs/GetLogsByDateRange?startDate={start}&endDate={end}
```

#### ✅ Después
```
GET    /api/logs                              → Obtener todos los logs
GET    /api/logs/{id}                         → Obtener log por ID
GET    /api/users/{userId}/logs               → Obtener logs de un usuario
GET    /api/logs/table/{tableName}            → Obtener logs por tabla
GET    /api/logs?startDate={start}&endDate={end} → Obtener logs por rango de fechas
```

---

### 17. AuthController

#### ✅ Ya está bien (mantener)
```
POST   /api/Auth/logIn         → Login de usuario
```

---

## 📊 Resumen de Cambios

| Aspecto | Antes | Después |
|---------|-------|---------|
| Nombres de rutas | PascalCase | kebab-case |
| Verbos en rutas | Sí (GetAll, Create) | No (solo sustantivos) |
| Jerarquía | Plana | Anidada cuando corresponde |
| Consistencia | Baja | Alta |
| RESTful | No | Sí |

---

## 🎯 Beneficios de la Estandarización

1. **Más intuitivo**: Los desarrolladores pueden predecir las rutas
2. **Estándar de industria**: Sigue convenciones REST ampliamente aceptadas
3. **Mejor documentación**: Swagger/OpenAPI se ve más profesional
4. **Escalabilidad**: Fácil agregar nuevos endpoints siguiendo el patrón
5. **Integración**: Más fácil para clientes frontend y móviles

---

## 🔄 Próximo Paso

Implementar estos cambios en todos los controladores manteniendo las políticas de autorización ya configuradas.
