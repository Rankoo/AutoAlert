import { Navigate, Outlet  } from "react-router"
import { useAuthStore } from "../store/authStore"
import { useCurrentUserInfo } from "./hooks/useCurrentUserInfo"
import { useEffect } from "react"
import { useCurrentUserInfoStore } from "../store/currentUserInfoStore"
import Loader from "../components/Loader"
import { Layout } from "../layout/Layout"

export const HomeRedirect = () => {

  const { currentUserInfoQuery } = useCurrentUserInfo()
  const { isAuthenticated } = useAuthStore()
  const { setUserInfo } = useCurrentUserInfoStore()
  console.log({ currentUserInfoQuery: currentUserInfoQuery.data })
  
  useEffect(() => {
    if (currentUserInfoQuery.data)
      setUserInfo(currentUserInfoQuery.data)
  }, [currentUserInfoQuery.data])
  
  if (currentUserInfoQuery.isLoading) {
    return <div className="w-full flex justify-center items-center h-screen">
      <Loader size="md" />
    </div>
  }
  
    
  if (!currentUserInfoQuery.data || !isAuthenticated) {
    return <Navigate to='/login' />
  }
  
  return (
    <>
      <Layout >
        <Outlet />
      </Layout>
      <p>Bienvenido, {currentUserInfoQuery.data.user.names} {currentUserInfoQuery.data.user.lastNames}!</p>
    </>
  )
}
