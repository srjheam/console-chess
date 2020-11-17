# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.3.0] - 2020-11-17

### Changed

- Method to show all the captured pieces.
- Refactored the project.

## [0.2.0] - 2020-11-11

### Added

- TwoDimensionPosition struct to represent a position in 2D spaces, focused on positions within arrays.
- A reference on each piece of the Board to which it belongs.
- BoardSide enum to find out which side the piece is from.
- An counter for the number of times a piece has moved.
- A property to get the position of the piece on the board to which it belongs.
- PieceMovement static class to handle all types of movements.
- Piece.PossibleTargets() that shows all targets of the piece by its movement rule.
- Capability to highlight all the possible targets for a piece on the board.
- Now the pieces can capture enemy pieces.
- On-screen indicator for previously captured pieces.

### Changed

- System.Drawing.Point has been replaced by TwoDimensionPosition struct.
- Now the movement of the piece is restricted by the movement rule of each piece.
- Each Piece.Team now has a different Console.ForegroundColor when printing it.

### Removed

- All uses of System.Drawing.Point struct.

### Fixed

- Misunderstandings about the x and y axes in an array.

## [0.1.0] - 2020-11-04

### Added

- The ability to receive, interpret and convert the position of the board informed by the user.
- GetPiece(), PlacePiece() and RemovePiece() methods to handle Board.squares[\*,\*].
- The ability to move pieces.

### Changed

- The method name from Board.PlacePieces() to SetupPieces().
- Board.Columns and Board.Rows to readonly.
- Board.squares[\*,\*] to private.

### Fixed

- ArgumentOutOfRangeException when handling null or empty strings in UserInput.IsBoardPosition() method.
- Unexpected exception message with the ArgumentOutOfRangeException in BoardPosition's constructor.

## [0.0.2] - 2020-11-02

### Added

- This CHANGELOG.
- Initial implementation of all the chessboard pieces.
- The ability to print the board on the console.

## [0.0.1] - 2020-10-28

### Added

- .gitignore.
- LICENSE.
- Initial README.
- Base of the C# console project.

[0.3.0]: https://github.com/srjheam/console-chess/compare/v0.2.0...v0.3.0
[0.2.0]: https://github.com/srjheam/console-chess/compare/v0.1.0...v0.2.0
[0.1.0]: https://github.com/srjheam/console-chess/compare/v0.0.2...v0.1.0
[0.0.2]: https://github.com/srjheam/console-chess/compare/v0.0.1...v0.0.2
[0.0.1]: https://github.com/srjheam/console-chess/releases/tag/v0.0.1
