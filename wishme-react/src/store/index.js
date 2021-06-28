import { configureStore } from "@reduxjs/toolkit";
import authSlice from "./auth-slice";
import eventSlice from "./event-slice";
import uiSlice from "./ui-slice";

const store = configureStore({
  reducer: {
    auth: authSlice.reducer,
    ui: uiSlice.reducer,
    event: eventSlice.reducer,
  },
});

export default store;
