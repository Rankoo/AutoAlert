import { useQuery } from "@tanstack/react-query"
import { getCurrentUserInfoAction } from "../../services/actions/authActions"

export const useCurrentUserInfo = () => {

  const currentUserInfoQuery = useQuery({
    queryKey: ["user","me"],
    queryFn: getCurrentUserInfoAction,
  })

  return {
    currentUserInfoQuery
  }
}
