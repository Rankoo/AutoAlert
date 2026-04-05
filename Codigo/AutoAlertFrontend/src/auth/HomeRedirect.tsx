import { Navigate, Outlet  } from "react-router"
import { useAuthStore } from "../store/authStore"
import { useCurrentUserInfo } from "./hooks/useCurrentUserInfo"
import { useEffect } from "react"
import { useCurrentUserInfoStore } from "../store/currentUserInfoStore"

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
    return <p>Loading...</p>
  }

  if (!currentUserInfoQuery.data || !isAuthenticated) {
    return <Navigate to='/login' />
  }
  
  return (
    <>
      <p>Bienvenido, {currentUserInfoQuery.data.user.names} {currentUserInfoQuery.data.user.lastNames}!</p>
      <Outlet />
    </>
  )
}
