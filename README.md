LaytonPeg is a program for solving the peg solitaire puzzle in Professor Layton and the Diabolical Box. LaytonSlide is a program for solving the final sliding piece puzzle. These are not intended to be used by anyone but myself; I just wanted to share. LaytonPeg runs very quickly. I had to do a little optimization with LaytonSlide to get it to use less memory and run faster. Switching from a stack to a queue made a big difference in the solution found, going from 10,430 moves to 188 moves. There's definitely more room for optimization but it runs to completion with solution small enough to be entered on the DS so that's good enough.

Initial board setup is hardcoded in each program's BoardState.cs.

Example LaytonPeg output:

```
  OOO
  OOO
OOOOOOO
OOO.OOO
OOOOOOO
  OOO
  OOO

[5, 3] -> [3, 3]
  OOO
  OOO
OOOOOOO
OOOOOOO
OOO.OOO
  O.O
  OOO

[4, 5] -> [4, 3]
  OOO
  OOO
OOOOOOO
OOOOOOO
OOOO..O
  O.O
  OOO
[...]
  ...
  .O.
...O...
.......
.......
  ...
  ...

[2, 3] -> [0, 3]
  .O.
  ...
.......
.......
.......
  ...
  ...
```

Example LaytonSlide output:

```
1000 positions evaluated.
2000 positions evaluated.
3000 positions evaluated.
[...]
146000 positions evaluated.
188 moves to win.
##OO###
##OO###
#c..d.#
ccc.dd#
.c.#__.
.ooIll#
#.oIl.#
##..###
##..###

darkBlueL left
##OO###
##OO###
#c.d..#
cccdd.#
.c.#__.
.ooIll#
#.oIl.#
##..###
##..###

_ right
##OO###
##OO###
#c.d..#
cccdd.#
.c.#.__
.ooIll#
#.oIl.#
##..###
##..###

orangeL left
##OO###
##OO###
#c.d..#
cccdd.#
.c.#.__
oo.Ill#
#o.Il.#
##..###
##..###
```