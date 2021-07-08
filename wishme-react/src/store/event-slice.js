import { createSlice } from "@reduxjs/toolkit";

const eventSlice = createSlice({
  name: "event",
  initialState: {
    lists: [],
    items: {},
  },
  reducers: {
    setLists(state, actions) {
      state.lists = actions.payload;
    },
    setItems(state, action) {
      state.items[action.payload.id] = action.payload.items;
    },
  },
});

export const eventActions = eventSlice.actions;

export default eventSlice;
