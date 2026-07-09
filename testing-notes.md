# Testing Notes

I tested status transitions because they are business rules.

I tested validation because invalid input should fail predictably.

## What I tested

- Creating a book with valid data.
- Rejecting a book with an empty title.
- Updating a book status with a valid transition.
- Rejecting an invalid status transition.
- Returning null when a book does not exist.

## Testing concepts practiced

- Arrange
- Act
- Assert
- Unit test
- Business logic test

## Why these tests matter

These tests protect the BookService from breaking important business rules.

The status transition tests are important because not every status change should be allowed.

The validation tests are important because invalid input should fail in a clear and predictable way.