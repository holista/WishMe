//import { useHistory } from "react-router-dom";
//import { useSelector } from "react-redux";

import { useParams, useRouteMatch } from "react-router";
import Item from "../components/item/Item";

const ItemPage = (props) => {
  const { evId, listId, id } = useParams();

  // const history = useHistory();
  // const evId = history.location.pathname.substring(
  //   history.location.pathname.lastIndexOf("/") + 1
  // );
  // const modalIsOpen = useSelector((state) => state.ui.modalIsOpen);

  return (
    <>
      <Item />
    </>
  );
};

export default ItemPage;
