import { BrowserRouter, Route, Routes } from "react-router";
import { Login } from "../auth/Login";
import { HomeRedirect } from "../auth/HomeRedirect";

export function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route element={<HomeRedirect />} >
          <>
            <Route path="/" element={<p>Home</p>} />
          </>
        </Route>
        {/* <Routere path="/" element={<HomeRedirect />} />

        <Route element={<GuestLayout />}>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/verification" element={<VerificationPage />} />
          <Route path="/forgot-password" element={<ForgotPasswordPage />} />
          <Route path="/reset-password" element={<ResetPasswordPage />} />
        </Route>

        <Route element={<ProtectedLayout />}>
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/users" element={<UserManagement />} />
          <Route path="/services" element={<Services />} />
          <Route path="/notifications" element={<Notifications />} />
          <Route path="/reports" element={<Reports />} />
          <Route path="/multicompany" element={<MultiCompany />} />
          <Route path="/support" element={<LiveSupport />} />
        </Route>

        <Route path="*" element={<HomeRedirect />} /> */}
      </Routes>
    </BrowserRouter>
  );
}
