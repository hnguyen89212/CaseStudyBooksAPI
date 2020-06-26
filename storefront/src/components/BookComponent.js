import React from "react";
import { Link } from "react-router-dom";

class BookComponent extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      book: props.book,
    };
  }

  render() {
    const toBookPage = {
      pathname: "/bookpage",
      book: this.state.book,
    };
    return (
      <tr>
        <td>
          <Link to={toBookPage}>
            <img
              className="thumbnail shadow p-3 mb-5 bg-white rounded"
              alt="placeholder"
              src={require(`../assets/${this.state.book.graphicName}`)}
            />
          </Link>
        </td>
        <td className="text-wrap text-justify">
          {this.state.book.productName}
          <br />
          <br />
          {/* {this.state.book.description} */}
          Donec quis nibh nibh. Quisque consequat turpis vel condimentum placerat. Phasellus vel justo turpis. Etiam lacus felis, tempor nec dui in, consectetur auctor diam. Sed facilisis interdum turpis, id egestas mi ultricies ut. Etiam sollicitudin mi nisl. Proin enim orci, bibendum et dui in, bibendum egestas lacus. Morbi consequat sollicitudin est, nec porta augue egestas eget. Mauris ac ex lobortis, consequat mi et, dignissim orci. Vestibulum sollicitudin orci quis tortor elementum sagittis. Quisque pulvinar vel metus luctus vulputate. Vestibulum eu arcu interdum, imperdiet urna congue, molestie ligula. Sed sit amet viverra metus.
        </td>
        <td>
          <Link to={toBookPage}>
            <button className="btn btn-primary" type="button">
              More Information...
            </button>
          </Link>
        </td>
      </tr>
    );
  }
}

export default BookComponent;
