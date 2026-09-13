import { 
  LayoutDashboard, 
  Users, 
  CreditCard, 
  MessageSquare, 
  BarChart3, 
  Store,
  Headphones,
  Zap
} from 'lucide-react';
import { useCurrentUserInfoStore } from '@/store/currentUserInfoStore';
import { getRolePermissions, type Permission } from '@/utils/permissions';
import { NavLink } from 'react-router';
interface SidebarProps {
  isOpen?: boolean;
}
export type ModuleType = 'dashboard' | 'users' | 'services' | 'stores' | 'notifications' | 'reports' | 'support';

export function Sidebar({ isOpen }: SidebarProps) {

  const userInfo = useCurrentUserInfoStore((state) => state.userInfo?.user);
  const permissions = userInfo?.permissions ?? [];
  const rolePermissions = getRolePermissions(userInfo?.role);
  const menuItems = [
  {
    id: 'users' as ModuleType,
    label: 'Gestión de Usuarios',
    path: '/users',
    icon: Users,
    permission: 'VIEW_USERS' as Permission,
  },
  {
    id: 'services' as ModuleType,
    label: 'Servicios',
    path: '/services',
    icon: CreditCard,
    permission: 'VIEW_SERVICES' as Permission,
  },
  {
    id: 'stores' as ModuleType,
    label: 'Tiendas',
    path: '/stores',
    icon: Store,
    permission: 'VIEW_STORES' as Permission,
  },
];

  const visibleMenuItems = menuItems.filter((item) =>
    permissions.includes(item.permission) || rolePermissions.includes(item.permission)
  );

  if (!isOpen) return null;

  return (
    <aside className="w-64 bg-white border-r border-gray-200 overflow-y-auto">
      <div className="p-6">
        <div className="flex flex-row items-center gap-2 mb-8">
          <div>
            <div className="relative">
              <svg width="50" height="50" viewBox="0 0 50 50" fill="none">
                <circle cx="25" cy="25" r="24" fill="#1E40AF" opacity="0.1"/>
                <path d="M25 10C16.716 10 10 16.716 10 25C10 33.284 16.716 40 25 40C33.284 40 40 33.284 40 25C40 16.716 33.284 10 25 10ZM25 12C32.203 12 38 17.797 38 25C38 32.203 32.203 38 25 38C17.797 38 12 32.203 12 25C12 17.797 17.797 12 25 12Z" fill="#1E40AF"/>
                <path d="M18 25L23 30L32 20" stroke="#1E40AF" strokeWidth="2.5" strokeLinecap="round" strokeLinejoin="round"/>
              </svg>
            </div>
          </div>
          <h1 className="text-red-600 text-3xl">Auto Alert</h1>
        </div>
        
        <nav className="space-y-1">
          {visibleMenuItems.map((item) => {
            const Icon = item.icon;
            return (
              <NavLink
                key={item.id}
                to={item.path}
                className={({ isActive }) => `w-full flex items-center gap-3 px-3 py-2.5 rounded-lg transition-colors ${
                  isActive ? 'bg-blue-50 text-blue-700' : 'text-gray-700 hover:bg-gray-50'
                }`}
              >
                {({ isActive }) => (
                  <>
                    <Icon className={`w-5 h-5 ${isActive ? 'text-blue-700' : 'text-gray-500'}`} />
                    <span className="text-sm">{item.label}</span>
                  </>
                )}
              </NavLink>
            );
          })}
        </nav>
      </div>
    </aside>
  );
}