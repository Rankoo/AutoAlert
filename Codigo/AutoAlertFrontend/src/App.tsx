import './styles/globals.css';
import { AppRouter } from "./router/router";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";

export default function App() {
  const queryClient = new QueryClient();

  return (
    <>
      <QueryClientProvider client={queryClient}>
        <AppRouter />
        <ReactQueryDevtools />
      </QueryClientProvider>
    </>
  );
}