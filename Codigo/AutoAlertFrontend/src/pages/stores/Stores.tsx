import { Store } from 'lucide-react';

export function Stores() {
  return (
    <section className="space-y-4">
      <div className="flex items-center gap-3">
        <Store className="h-6 w-6 text-blue-700" />
        <div>
          <h2 className="text-gray-900">Tiendas</h2>
          <p className="text-gray-500">Gestiona las tiendas registradas.</p>
        </div>
      </div>
    </section>
  );
}