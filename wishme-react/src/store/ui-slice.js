import { createSlice } from "@reduxjs/toolkit";

const uiSlice = createSlice({
  name: "ui",
  initialState: { modalIsOpen: false },
  reducers: {
    openModal(state) {
      state.modalIsOpen = true;
    },
    closeModal(state) {
      state.modalIsOpen = false;
    },
  },
});

export const uiActions = uiSlice.actions;

export default uiSlice;
