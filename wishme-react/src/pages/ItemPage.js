import { useParams } from "react-router";
import Item from "../components/item/Item";

const ItemPage = (props) => {
  const { evId, listId, id } = useParams();

  return (
    <>
      <Item />
    </>
  );
};

export default ItemPage;
