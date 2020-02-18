# Changelog

## [0.2.1] - 2020-02-17
- Added Byte and Ushort to bool matrix functions
- Added int4x4 select wrapper
- style adjustments to be more in line with Unity standards

## [0.2.0] - 2019-08-19
- Fixed some naming inconsistencies with BitPack functions and test suite.
- Added BitUtils.Swap functions with overloads for byte, ushort, uint, and ulong.
- Added complete BitUtils Test Suite

## [0.1.2] - 2019-08-12
- Add missing `BitPack` ulong getters and setters. Should have been
  there from the start.
- Fixed bug with `BitPack.UIntMask(byte)` and `BitPack.ULongMask(byte)`
  not returning correct values due to incorrect bit truncation.
- Added Test suite for `BitPack` class for full coverage.

## [0.1.1] - 2019-08-11
- Minor adjustments to the package file and naming scheme.

## [0.1.0] - 2019-08-10 
Initial Commit, split off from main game repo.

Added the BitPack utilities.