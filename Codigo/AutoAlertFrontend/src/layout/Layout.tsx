import { useState } from "react"
import { Header } from "./components/Header"
import { Sidebar } from "./components/Sidebar"

interface LayoutProps {
  children: React.ReactNode
}

export const Layout = ({ children }: LayoutProps) => {
  
    const [openSidebar, setOpenSidebar] = useState<boolean>(true)

   const toggleSidebar = () => {
    setOpenSidebar(!openSidebar)
   }
    return (
    <div className="flex h-screen bg-gray-50">
      <Sidebar
        isOpen={openSidebar}
      />
      <div className="flex flex-col flex-1 overflow-hidden">
        <Header toggleSidebar={toggleSidebar} />
        <main className="flex-1 overflow-y-auto p-6">
          {children}
        </main>
      </div>
    </div>
  )
}
