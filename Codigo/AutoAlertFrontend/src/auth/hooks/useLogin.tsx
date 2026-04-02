import { useState } from "react";
import { useForm } from "react-hook-form";

type FormValues = {
  user: string
  password: string
}

export const useLogin = () => {
  const [showPassword, setShowPassword] = useState(false)
  const [rememberMe, setRememberMe] = useState(false)

  const toggleShowPassword = () => {
    setShowPassword((prev) => !prev)
  }

  const { register, handleSubmit } = useForm<FormValues>({ defaultValues: {user: '', password: ''}})

  const onSubmit = handleSubmit((data) => console.log({ data }));

  
  return {
    toggleShowPassword,
    showPassword,
    rememberMe,
    setRememberMe,
    register,
    onSubmit
  }
}
