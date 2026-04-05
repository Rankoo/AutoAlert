import { useQuery } from "@tanstack/react-query"
import { getCurrentUserInfoAction } from "../../services/actions/authActions"

export const useCurrentUserInfo = () => {

  const currentUserInfoQuery = useQuery({
    queryKey: ["user","me"],
    queryFn: getCurrentUserInfoAction,
    staleTime: 2 * 60 * 60 * 1000,
  })

  return {
    currentUserInfoQuery
  }
}
