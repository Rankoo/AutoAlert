import { Navigate } from "react-router"
import { useAuthStore } from "../store/authStore"

export const HomeRedirect = () => {

  const { isAuthenticated } = useAuthStore()

  if(!isAuthenticated)
    return <Navigate to='/login' />

  return (
    <div>HomeRedirect</div>
  )
}
