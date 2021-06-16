import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
  name: "auth",
  initialState: {
    isRegistered: false,
    isAuthenticated: false,
    isOrganizer: false,
    token: null,
    organizerId: null,
  },
  reducers: {
    login(state, action) {
      state.isAuthenticated = true;
      state.token = action.payload.token;
      state.organizerId = action.payload.organizerId;
    },
    logout(state) {
      state.isAuthenticated = false;
      state.token = null;
    },
    register(state, action) {
      state.isRegistered = true;
      state.token = action.payload.token;
      state.organizerId = action.payload.organizerId;
    },
    toggle(state) {
      state.isRegistered = !state.isRegistered;
    },
  },
});

export const authActions = authSlice.actions;

export default authSlice;
