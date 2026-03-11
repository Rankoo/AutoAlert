# 🗄️ Configuración de Base de Datos - AutoAlert

## 📋 Scripts Disponibles

### 1. `scriptAutoAlertDB.sql` - Creación de Base de Datos
**Propósito**: Crea la estructura completa de la base de datos AutoAlertDB

**Contenido:**
- Creación de base de datos
- 16 tablas principales
- Relaciones y constraints
- Índices y claves foráneas

**Ejecución:**
```bash
sqlcmd -S localhost -i "scriptAutoAlertDB.sql"
```

---

### 2. `02_ConfigurarModulosYPermisos.sql` - Configuración de Permisos
**Propósito**: Configura módulos, submódulos y permisos del sistema

**Contenido:**
- 14 Módulos del sistema
- 61 SubMódulos (Permisos)
- Configuración del rol Administrador
- Asignación automática de permisos

**Ejecución:**
```bash
sqlcmd -S localhost -d AutoAlertDB -i "02_ConfigurarModulosYPermisos.sql"
```

---

## 🚀 Guía de Instalación Completa

### Paso 1: Crear la Base de Datos

```bash
# Opción 1: Usando sqlcmd
sqlcmd -S localhost -i "scriptAutoAlertDB.sql"

# Opción 2: Usando SSMS
# 1. Abrir SQL Server Management Studio
# 2. Conectarse al servidor
# 3. Abrir scriptAutoAlertDB.sql
# 4. Ejecutar (F5)
```

**Verificación:**
```sql
-- Verificar que la base de datos existe
SELECT name FROM sys.databases WHERE name = 'AutoAlertDB';

-- Verificar tablas creadas
USE AutoAlertDB;
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';
```

---

### Paso 2: Configurar Módulos y Permisos

```bash
# Ejecutar script de configuración
sqlcmd -S localhost -d AutoAlertDB -i "02_ConfigurarModulosYPermisos.sql"
```

**Verificación:**
```sql
USE AutoAlertDB;

-- Verificar módulos (debe retornar 14)
SELECT COUNT(*) AS TotalModulos FROM Modules WHERE IsActive = 1;

-- Verificar permisos (debe retornar 61)
SELECT COUNT(*) AS TotalPermisos FROM SubModules WHERE IsActive = 1;

-- Verificar rol Administrador
SELECT * FROM Roles WHERE Name = 'Administrador';

-- Verificar permisos del Administrador (debe retornar 61)
SELECT COUNT(*) AS PermisosAdmin 
FROM RoleSubModules rsm
INNER JOIN Roles r ON rsm.RoleId = r.Id
WHERE r.Name = 'Administrador' AND rsm.IsActive = 1;
```

---

### Paso 3: Crear Usuario Administrador Inicial

```sql
USE AutoAlertDB;

-- Obtener ID del rol Administrador
DECLARE @AdminRoleId UNIQUEIDENTIFIER;
SELECT @AdminRoleId = Id FROM Roles WHERE Name = 'Administrador';

-- Obtener ID del tipo de documento (ajustar según tus datos)
DECLARE @DocumentTypeId UNIQUEIDENTIFIER;
SELECT TOP 1 @DocumentTypeId = Id FROM DocumentTypes;

-- Crear usuario administrador
-- NOTA: El password debe ser hasheado con BCrypt en el backend
INSERT INTO Users (
    Id, 
    DocumentTypeId, 
    DocumentNumber, 
    Names, 
    LastNames, 
    Email, 
    PasswordHash, 
    Phone, 
    RoleId, 
    CreatedAt, 
    UpdatedAt, 
    IsActive
)
VALUES (
    NEWID(),
    @DocumentTypeId,
    '1234567890',
    'Administrador',
    'Sistema',
    'admin@autoalert.com',
    '$2a$11$YourBCryptHashedPasswordHere', -- Cambiar por hash real
    '3001234567',
    @AdminRoleId,
    GETDATE(),
    GETDATE(),
    1
);

PRINT 'Usuario administrador creado';
```

**Nota Importante**: El password debe ser hasheado usando BCrypt. Puedes crear el usuario desde el endpoint `/api/users` del backend.

---

## 📊 Estructura de Módulos y Permisos

### Resumen por Módulo

| Módulo | Permisos | Descripción |
|--------|----------|-------------|
| Usuarios | 4 | VIEW, CREATE, EDIT, DELETE |
| Roles | 4 | VIEW, CREATE, EDIT, DELETE |
| Empresas | 4 | VIEW, CREATE, EDIT, DELETE |
| Grupos | 4 | VIEW, CREATE, EDIT, DELETE |
| Tiendas | 4 | VIEW, CREATE, EDIT, DELETE |
| Servicios | 4 | VIEW, CREATE, EDIT, DELETE |
| Alertas | 4 | VIEW, CREATE, EDIT, DELETE |
| Notificaciones | 4 | VIEW, CREATE, EDIT, DELETE |
| Módulos | 4 | VIEW, CREATE, EDIT, DELETE |
| SubMódulos | 4 | VIEW, CREATE, EDIT, DELETE |
| Tipos de Documento | 4 | VIEW, CREATE, EDIT, DELETE |
| Permisos | 2 | VIEW, UPDATE |
| Relaciones | 6 | VIEW, CREATE, DELETE (User-Company/Group) |
| Auditoría | 1 | VIEW (solo lectura) |

**Total: 14 Módulos, 61 Permisos**

---

## 🔍 Consultas Útiles

### Ver Todos los Módulos
```sql
SELECT 
    m.Name AS Modulo,
    m.Description,
    m.Icon,
    m.[Order],
    COUNT(sm.Id) AS CantidadPermisos
FROM Modules m
LEFT JOIN SubModules sm ON m.Id = sm.ModuleId AND sm.IsActive = 1
WHERE m.IsActive = 1
GROUP BY m.Name, m.Description, m.Icon, m.[Order]
ORDER BY m.[Order];
```

### Ver Todos los Permisos
```sql
SELECT 
    m.Name AS Modulo,
    sm.Name AS Permiso,
    sm.Description,
    sm.Route,
    sm.[Order]
FROM SubModules sm
INNER JOIN Modules m ON sm.ModuleId = m.Id
WHERE sm.IsActive = 1
ORDER BY m.[Order], sm.[Order];
```

### Ver Permisos de un Rol Específico
```sql
DECLARE @RoleName NVARCHAR(100) = 'Administrador';

SELECT 
    m.Name AS Modulo,
    sm.Name AS Permiso,
    rsm.CanView AS Ver,
    rsm.CanCreate AS Crear,
    rsm.CanEdit AS Editar,
    rsm.CanDelete AS Eliminar
FROM RoleSubModules rsm
INNER JOIN SubModules sm ON rsm.SubModuleId = sm.Id
INNER JOIN Modules m ON sm.ModuleId = m.Id
INNER JOIN Roles r ON rsm.RoleId = r.Id
WHERE r.Name = @RoleName AND rsm.IsActive = 1
ORDER BY m.[Order], sm.[Order];
```

### Ver Permisos de un Usuario Específico
```sql
DECLARE @UserEmail NVARCHAR(255) = 'admin@autoalert.com';

-- Permisos del rol del usuario
SELECT 
    'Rol' AS Origen,
    m.Name AS Modulo,
    sm.Name AS Permiso,
    rsm.CanView AS Ver,
    rsm.CanCreate AS Crear,
    rsm.CanEdit AS Editar,
    rsm.CanDelete AS Eliminar
FROM Users u
INNER JOIN Roles r ON u.RoleId = r.Id
INNER JOIN RoleSubModules rsm ON r.Id = rsm.RoleId
INNER JOIN SubModules sm ON rsm.SubModuleId = sm.Id
INNER JOIN Modules m ON sm.ModuleId = m.Id
WHERE u.Email = @UserEmail AND rsm.IsActive = 1

UNION ALL

-- Permisos personalizados del usuario
SELECT 
    'Usuario' AS Origen,
    m.Name AS Modulo,
    sm.Name AS Permiso,
    usm.CanView AS Ver,
    usm.CanCreate AS Crear,
    usm.CanEdit AS Editar,
    usm.CanDelete AS Eliminar
FROM Users u
INNER JOIN UserSubmodules usm ON u.Id = usm.UserId
INNER JOIN SubModules sm ON usm.SubModuleId = sm.Id
INNER JOIN Modules m ON sm.ModuleId = m.Id
WHERE u.Email = @UserEmail AND usm.IsActive = 1
ORDER BY Modulo, Permiso;
```

---

## 🛠️ Mantenimiento

### Agregar un Nuevo Módulo

```sql
-- 1. Crear el módulo
DECLARE @NewModuleId UNIQUEIDENTIFIER = NEWID();
INSERT INTO Modules (Id, Name, Description, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES (@NewModuleId, 'NuevoModulo', 'Descripción del módulo', 'icon-name', 15, GETDATE(), GETDATE(), 1);

-- 2. Agregar permisos al módulo
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @NewModuleId, 'VIEW_NUEVO', 'Ver nuevo módulo', '/api/nuevo', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @NewModuleId, 'CREATE_NUEVO', 'Crear en nuevo módulo', '/api/nuevo', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @NewModuleId, 'EDIT_NUEVO', 'Editar en nuevo módulo', '/api/nuevo', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @NewModuleId, 'DELETE_NUEVO', 'Eliminar en nuevo módulo', '/api/nuevo', 'trash', 4, GETDATE(), GETDATE(), 1);

-- 3. Asignar permisos al Administrador
DECLARE @AdminRoleId UNIQUEIDENTIFIER;
SELECT @AdminRoleId = Id FROM Roles WHERE Name = 'Administrador';

INSERT INTO RoleSubModules (Id, RoleId, SubModuleId, CanView, CanCreate, CanEdit, CanDelete, CreatedAt, UpdatedAt, IsActive)
SELECT 
    NEWID(),
    @AdminRoleId,
    sm.Id,
    1, 1, 1, 1,
    GETDATE(), GETDATE(), 1
FROM SubModules sm
WHERE sm.ModuleId = @NewModuleId;
```

### Crear un Nuevo Rol

```sql
-- 1. Crear el rol
DECLARE @NewRoleId UNIQUEIDENTIFIER = NEWID();
INSERT INTO Roles (Id, Name, Description, CreatedAt, UpdatedAt, IsActive)
VALUES (@NewRoleId, 'NuevoRol', 'Descripción del rol', GETDATE(), GETDATE(), 1);

-- 2. Asignar permisos específicos
-- Ejemplo: Solo permisos de lectura
INSERT INTO RoleSubModules (Id, RoleId, SubModuleId, CanView, CanCreate, CanEdit, CanDelete, CreatedAt, UpdatedAt, IsActive)
SELECT 
    NEWID(),
    @NewRoleId,
    sm.Id,
    1, -- CanView = true
    0, -- CanCreate = false
    0, -- CanEdit = false
    0, -- CanDelete = false
    GETDATE(), GETDATE(), 1
FROM SubModules sm
WHERE sm.Name LIKE 'VIEW_%';
```

---

## 🔐 Seguridad

### Recomendaciones

1. **Passwords**: Siempre usar BCrypt para hashear passwords
2. **Conexión**: Usar conexiones seguras (SSL/TLS)
3. **Backups**: Realizar backups regulares de la base de datos
4. **Auditoría**: Revisar logs periódicamente
5. **Permisos**: Aplicar principio de menor privilegio

### Backup de Base de Datos

```sql
-- Backup completo
BACKUP DATABASE AutoAlertDB
TO DISK = 'C:\Backups\AutoAlertDB_Full.bak'
WITH FORMAT, INIT, NAME = 'AutoAlertDB-Full Database Backup';

-- Backup diferencial
BACKUP DATABASE AutoAlertDB
TO DISK = 'C:\Backups\AutoAlertDB_Diff.bak'
WITH DIFFERENTIAL, INIT, NAME = 'AutoAlertDB-Differential Database Backup';
```

---

## 📚 Documentación Relacionada

- `CONFIGURACION_MODULOS_Y_POLITICAS.md` - Guía completa de configuración
- `IMPLEMENTACION_COMPLETA_SEGURIDAD.md` - Sistema de autorización
- `ENDPOINTS_RESTFUL_FINALES.md` - Documentación de API

---

## ✅ Checklist de Instalación

- [ ] Ejecutar `scriptAutoAlertDB.sql`
- [ ] Verificar creación de 16 tablas
- [ ] Ejecutar `02_ConfigurarModulosYPermisos.sql`
- [ ] Verificar 14 módulos creados
- [ ] Verificar 61 permisos creados
- [ ] Verificar rol Administrador configurado
- [ ] Crear usuario administrador inicial
- [ ] Probar login desde el backend
- [ ] Verificar permisos funcionando
- [ ] Configurar backup automático

---

## 🆘 Solución de Problemas

### Error: Base de datos ya existe
```sql
-- Eliminar base de datos existente (¡CUIDADO! Perderás todos los datos)
DROP DATABASE AutoAlertDB;
GO
-- Luego ejecutar scriptAutoAlertDB.sql nuevamente
```

### Error: Rol Administrador no existe
```sql
-- Crear rol manualmente
INSERT INTO Roles (Id, Name, Description, CreatedAt, UpdatedAt, IsActive)
VALUES (NEWID(), 'Administrador', 'Rol con acceso completo', GETDATE(), GETDATE(), 1);
```

### Error: Permisos duplicados
```sql
-- Limpiar permisos duplicados
DELETE FROM RoleSubModules
WHERE Id NOT IN (
    SELECT MIN(Id)
    FROM RoleSubModules
    GROUP BY RoleId, SubModuleId
);
```

---

**¡Base de datos lista para producción! 🚀**
