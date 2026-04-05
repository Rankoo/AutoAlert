import { create } from 'zustand';
import type { CurrentUserInfo } from '../services/actions/authActions';

type CurrentUserInfoState = {
  userInfo?: CurrentUserInfo | undefined;
  setUserInfo: (userInfo: CurrentUserInfo) => void;
}

export const useCurrentUserInfoStore = create<CurrentUserInfoState>((set) => ({
  userInfo: undefined,
  setUserInfo: (userInfo: CurrentUserInfo) => set({ userInfo }),
}));