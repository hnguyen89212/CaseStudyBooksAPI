import React from "react";
import BookComponent from "./BookComponent";

class BookList extends React.Component {
  render() {
    return (
      <table className="table table-hover table-responsive-sm my-3">
        <thead>
          <tr>
            <th>Cover</th>
            <th>Title</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {this.props.books.map((eachBook) => (
            <BookComponent
              book={eachBook}
              key={eachBook.productName}
              onClick={() => this.handleClick()}
            />
          ))}
        </tbody>
      </table>
    );
  }
}

export default BookList;
