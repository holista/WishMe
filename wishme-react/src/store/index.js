import { configureStore } from "@reduxjs/toolkit";
import authSlice from "./auth-slice";
import eventSlice from "./event-slice";

const store = configureStore({
  reducer: {
    auth: authSlice.reducer,
    event: eventSlice.reducer,
  },
});

export default store;
