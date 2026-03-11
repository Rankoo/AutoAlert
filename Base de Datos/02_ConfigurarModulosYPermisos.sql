-- =============================================
-- Script: Configuración de Módulos y Permisos
-- Base de Datos: AutoAlertDB
-- Descripción: Crea los módulos, submódulos y permisos del sistema AutoAlert
-- Versión: 1.0
-- Fecha: 2025
-- =============================================

USE AutoAlertDB;
GO

-- =============================================
-- PASO 1: Limpiar datos existentes (opcional - comentar si no deseas limpiar)
-- =============================================
/*
DELETE FROM UserSubmodules;
DELETE FROM RoleSubModules;
DELETE FROM SubModules;
DELETE FROM Modules;
*/

-- =============================================
-- PASO 2: Insertar Módulos Principales
-- =============================================

DECLARE @ModuleIdUsuarios UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdRoles UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdEmpresas UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdGrupos UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdTiendas UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdServicios UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdAlertas UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdNotificaciones UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdModulos UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdSubModulos UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdTiposDocumento UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdPermisos UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdRelaciones UNIQUEIDENTIFIER = NEWID();
DECLARE @ModuleIdAuditoria UNIQUEIDENTIFIER = NEWID();

-- Insertar Módulos
INSERT INTO Modules (Id, Name, Description, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (@ModuleIdUsuarios, 'Usuarios', 'Gestión de usuarios del sistema', 'users', 1, GETDATE(), GETDATE(), 1),
    (@ModuleIdRoles, 'Roles', 'Gestión de roles y perfiles', 'shield', 2, GETDATE(), GETDATE(), 1),
    (@ModuleIdEmpresas, 'Empresas', 'Gestión de empresas', 'building', 3, GETDATE(), GETDATE(), 1),
    (@ModuleIdGrupos, 'Grupos', 'Gestión de grupos de tiendas', 'users-group', 4, GETDATE(), GETDATE(), 1),
    (@ModuleIdTiendas, 'Tiendas', 'Gestión de tiendas y puntos de venta', 'store', 5, GETDATE(), GETDATE(), 1),
    (@ModuleIdServicios, 'Servicios', 'Gestión de servicios públicos', 'plug', 6, GETDATE(), GETDATE(), 1),
    (@ModuleIdAlertas, 'Alertas', 'Gestión de alertas programadas', 'bell', 7, GETDATE(), GETDATE(), 1),
    (@ModuleIdNotificaciones, 'Notificaciones', 'Gestión de notificaciones', 'mail', 8, GETDATE(), GETDATE(), 1),
    (@ModuleIdModulos, 'Módulos', 'Gestión de módulos del sistema', 'grid', 9, GETDATE(), GETDATE(), 1),
    (@ModuleIdSubModulos, 'SubMódulos', 'Gestión de submódulos', 'list', 10, GETDATE(), GETDATE(), 1),
    (@ModuleIdTiposDocumento, 'Tipos de Documento', 'Gestión de tipos de documento', 'file-text', 11, GETDATE(), GETDATE(), 1),
    (@ModuleIdPermisos, 'Permisos', 'Gestión de permisos y accesos', 'lock', 12, GETDATE(), GETDATE(), 1),
    (@ModuleIdRelaciones, 'Relaciones', 'Gestión de relaciones usuario-empresa/grupo', 'link', 13, GETDATE(), GETDATE(), 1),
    (@ModuleIdAuditoria, 'Auditoría', 'Logs y auditoría del sistema', 'file-search', 14, GETDATE(), GETDATE(), 1);

PRINT 'Módulos insertados correctamente';

-- =============================================
-- PASO 3: Insertar SubMódulos (Permisos)
-- =============================================

-- SubMódulos de Usuarios
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdUsuarios, 'VIEW_USERS', 'Ver usuarios', '/api/users', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdUsuarios, 'CREATE_USERS', 'Crear usuarios', '/api/users', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdUsuarios, 'EDIT_USERS', 'Editar usuarios', '/api/users', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdUsuarios, 'DELETE_USERS', 'Eliminar usuarios', '/api/users', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Roles
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdRoles, 'VIEW_ROLES', 'Ver roles', '/api/roles', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdRoles, 'CREATE_ROLES', 'Crear roles', '/api/roles', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdRoles, 'EDIT_ROLES', 'Editar roles', '/api/roles', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdRoles, 'DELETE_ROLES', 'Eliminar roles', '/api/roles', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Empresas
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdEmpresas, 'VIEW_COMPANIES', 'Ver empresas', '/api/companies', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdEmpresas, 'CREATE_COMPANIES', 'Crear empresas', '/api/companies', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdEmpresas, 'EDIT_COMPANIES', 'Editar empresas', '/api/companies', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdEmpresas, 'DELETE_COMPANIES', 'Eliminar empresas', '/api/companies', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Grupos
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdGrupos, 'VIEW_GROUPS', 'Ver grupos', '/api/groups', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdGrupos, 'CREATE_GROUPS', 'Crear grupos', '/api/groups', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdGrupos, 'EDIT_GROUPS', 'Editar grupos', '/api/groups', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdGrupos, 'DELETE_GROUPS', 'Eliminar grupos', '/api/groups', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Tiendas
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdTiendas, 'VIEW_STORES', 'Ver tiendas', '/api/stores', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdTiendas, 'CREATE_STORES', 'Crear tiendas', '/api/stores', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdTiendas, 'EDIT_STORES', 'Editar tiendas', '/api/stores', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdTiendas, 'DELETE_STORES', 'Eliminar tiendas', '/api/stores', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Servicios
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdServicios, 'VIEW_SERVICES', 'Ver servicios', '/api/services', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdServicios, 'CREATE_SERVICES', 'Crear servicios', '/api/services', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdServicios, 'EDIT_SERVICES', 'Editar servicios', '/api/services', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdServicios, 'DELETE_SERVICES', 'Eliminar servicios', '/api/services', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Alertas
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdAlertas, 'VIEW_ALERTS', 'Ver alertas', '/api/alerts', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdAlertas, 'CREATE_ALERTS', 'Crear alertas', '/api/alerts', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdAlertas, 'EDIT_ALERTS', 'Editar alertas', '/api/alerts', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdAlertas, 'DELETE_ALERTS', 'Eliminar alertas', '/api/alerts', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Notificaciones
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdNotificaciones, 'VIEW_NOTIFICATIONS', 'Ver notificaciones', '/api/notifications', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdNotificaciones, 'CREATE_NOTIFICATIONS', 'Crear notificaciones', '/api/notifications', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdNotificaciones, 'EDIT_NOTIFICATIONS', 'Editar notificaciones', '/api/notifications', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdNotificaciones, 'DELETE_NOTIFICATIONS', 'Eliminar notificaciones', '/api/notifications', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Módulos
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdModulos, 'VIEW_MODULES', 'Ver módulos', '/api/modules', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdModulos, 'CREATE_MODULES', 'Crear módulos', '/api/modules', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdModulos, 'EDIT_MODULES', 'Editar módulos', '/api/modules', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdModulos, 'DELETE_MODULES', 'Eliminar módulos', '/api/modules', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de SubMódulos
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdSubModulos, 'VIEW_SUBMODULES', 'Ver submódulos', '/api/submodules', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdSubModulos, 'CREATE_SUBMODULES', 'Crear submódulos', '/api/submodules', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdSubModulos, 'EDIT_SUBMODULES', 'Editar submódulos', '/api/submodules', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdSubModulos, 'DELETE_SUBMODULES', 'Eliminar submódulos', '/api/submodules', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Tipos de Documento
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdTiposDocumento, 'VIEW_DOCUMENT_TYPES', 'Ver tipos de documento', '/api/document-types', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdTiposDocumento, 'CREATE_DOCUMENT_TYPES', 'Crear tipos de documento', '/api/document-types', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdTiposDocumento, 'EDIT_DOCUMENT_TYPES', 'Editar tipos de documento', '/api/document-types', 'edit', 3, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdTiposDocumento, 'DELETE_DOCUMENT_TYPES', 'Eliminar tipos de documento', '/api/document-types', 'trash', 4, GETDATE(), GETDATE(), 1);

-- SubMódulos de Permisos
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdPermisos, 'VIEW_PERMISSIONS', 'Ver permisos', '/api/permissions', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdPermisos, 'UPDATE_PERMISSIONS', 'Actualizar permisos', '/api/permissions', 'edit', 2, GETDATE(), GETDATE(), 1);

-- SubMódulos de Relaciones Usuario-Empresa
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdRelaciones, 'VIEW_USER_COMPANIES', 'Ver relaciones usuario-empresa', '/api/user-companies', 'eye', 1, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdRelaciones, 'CREATE_USER_COMPANIES', 'Crear relaciones usuario-empresa', '/api/user-companies', 'plus', 2, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdRelaciones, 'DELETE_USER_COMPANIES', 'Eliminar relaciones usuario-empresa', '/api/user-companies', 'trash', 3, GETDATE(), GETDATE(), 1);

-- SubMódulos de Relaciones Usuario-Grupo
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdRelaciones, 'VIEW_USER_GROUPS', 'Ver relaciones usuario-grupo', '/api/user-groups', 'eye', 4, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdRelaciones, 'CREATE_USER_GROUPS', 'Crear relaciones usuario-grupo', '/api/user-groups', 'plus', 5, GETDATE(), GETDATE(), 1),
    (NEWID(), @ModuleIdRelaciones, 'DELETE_USER_GROUPS', 'Eliminar relaciones usuario-grupo', '/api/user-groups', 'trash', 6, GETDATE(), GETDATE(), 1);

-- SubMódulos de Auditoría
INSERT INTO SubModules (Id, ModuleId, Name, Description, Route, Icon, [Order], CreatedAt, UpdatedAt, IsActive)
VALUES 
    (NEWID(), @ModuleIdAuditoria, 'VIEW_LOGS', 'Ver logs del sistema', '/api/logs', 'eye', 1, GETDATE(), GETDATE(), 1);

PRINT 'SubMódulos (Permisos) insertados correctamente';

-- =============================================
-- PASO 4: Asignar todos los permisos al rol Administrador
-- =============================================

-- Obtener el ID del rol Administrador (ajustar según tu base de datos)
DECLARE @AdminRoleId UNIQUEIDENTIFIER;

-- Buscar el rol Administrador por nombre
SELECT @AdminRoleId = Id FROM Roles WHERE Name = 'Administrador' OR Name = 'Admin';

-- Si no existe, crear el rol Administrador
IF @AdminRoleId IS NULL
BEGIN
    SET @AdminRoleId = NEWID();
    INSERT INTO Roles (Id, Name, Description, CreatedAt, UpdatedAt, IsActive)
    VALUES (@AdminRoleId, 'Administrador', 'Rol con acceso completo al sistema', GETDATE(), GETDATE(), 1);
    PRINT 'Rol Administrador creado';
END

-- Asignar todos los submódulos al rol Administrador con todos los permisos
INSERT INTO RoleSubModules (Id, RoleId, SubModuleId, CanView, CanCreate, CanEdit, CanDelete, CreatedAt, UpdatedAt, IsActive)
SELECT 
    NEWID(),
    @AdminRoleId,
    sm.Id,
    1, -- CanView
    1, -- CanCreate
    1, -- CanEdit
    1, -- CanDelete
    GETDATE(),
    GETDATE(),
    1
FROM SubModules sm
WHERE NOT EXISTS (
    SELECT 1 FROM RoleSubModules rsm 
    WHERE rsm.RoleId = @AdminRoleId AND rsm.SubModuleId = sm.Id
);

PRINT 'Permisos asignados al rol Administrador';

-- =============================================
-- PASO 5: Verificar la configuración
-- =============================================

-- Contar módulos
DECLARE @ModuleCount INT;
SELECT @ModuleCount = COUNT(*) FROM Modules WHERE IsActive = 1;
PRINT 'Total de Módulos activos: ' + CAST(@ModuleCount AS VARCHAR);

-- Contar submódulos
DECLARE @SubModuleCount INT;
SELECT @SubModuleCount = COUNT(*) FROM SubModules WHERE IsActive = 1;
PRINT 'Total de SubMódulos (Permisos) activos: ' + CAST(@SubModuleCount AS VARCHAR);

-- Contar permisos del Administrador
DECLARE @AdminPermissionCount INT;
SELECT @AdminPermissionCount = COUNT(*) 
FROM RoleSubModules 
WHERE RoleId = @AdminRoleId AND IsActive = 1;
PRINT 'Total de Permisos asignados al Administrador: ' + CAST(@AdminPermissionCount AS VARCHAR);

-- =============================================
-- PASO 6: Mostrar resumen de módulos y permisos
-- =============================================

SELECT 
    m.Name AS Modulo,
    COUNT(sm.Id) AS CantidadPermisos
FROM Modules m
LEFT JOIN SubModules sm ON m.Id = sm.ModuleId AND sm.IsActive = 1
WHERE m.IsActive = 1
GROUP BY m.Name, m.[Order]
ORDER BY m.[Order];

PRINT '=============================================';
PRINT 'Configuración de Módulos y Permisos completada exitosamente!';
PRINT '=============================================';
