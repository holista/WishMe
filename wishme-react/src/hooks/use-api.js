import { useCallback, useState } from "react";

const useApi = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  const sendRequest = useCallback(async (requestConfig, applyData) => {
    setIsLoading(true);
    setError(null);

    try {
      await fetch(`http://localhost:8085/api/v1/${requestConfig.url}`, {
        method: requestConfig.method ? requestConfig.method : "GET",
        body: requestConfig.body && requestConfig.body,
        headers: {
          accept: "application/json",
          "Content-Type": "application/json",
          ...requestConfig.headers,
        },
      })
        .then((res) => {
          if (!res.ok) {
            throw new Error("Něco se pokazilo");
          }
          return res.json();
        })
        .then((response) => {
          setIsLoading(false);
          applyData(response);
        });
    } catch (error) {
      setIsLoading(false);
      setError(error.message || "Něco se tu pokazilo");
    }
  }, []);

  return {
    isLoading,
    error,
    sendRequest,
  };
};

export default useApi;
