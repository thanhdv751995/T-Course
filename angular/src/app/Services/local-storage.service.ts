export const localStor = {
  writeAccessToken: (accessToken) => {
      localStorage.setItem("accessToken", accessToken);
  },

  writeRefreshToken: (refreshToken) => {
      localStorage.setItem("refreshToken", refreshToken);
  },
  getToken(): string{
      return localStorage.getItem("accessToken");
  },
  setLanguage(language){
      localStorage.setItem('abpSession',JSON.stringify({"language":language}));
  }
};