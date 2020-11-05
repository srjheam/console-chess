# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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

[0.1.0]: https://github.com/srjheam/console-chess/compare/v0.0.2...v0.1.0
[0.0.2]: https://github.com/srjheam/console-chess/compare/v0.0.1...v0.0.2
[0.0.1]: https://github.com/srjheam/console-chess/releases/tag/v0.0.1
