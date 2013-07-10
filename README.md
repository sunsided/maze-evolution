#MazeEvolution - Maze Solving using Evolutionary Programming

A simulation tool to generate maze-solving algorithms by means of evolutionary programming.

<p align="center">
  <img src="https://raw.github.com/sunsided/maze-evolution/master/screenshot.jpg" alt="Screenshot"/>
</p>

### Fitness evaluation

After each generation, each proband gets assigned a fitness value depending on the effective steps taken in the maze and the actual reaching of the target. The fitness is evaluated as follows:

  * if the proband reached the target `big number - stepsTaken`
  * if the proband did not reach the targen: `stepsTaken - big number`

This way, faster probands will be evaluated better, while probands not reaching the goal get weightend on their ability to try not to starve.

Probands that did not move (i.e. are starved) will be removed.

### Methods available

Each proband has a set of methods and boolean functions, tagged with the `[EvolutionaryMethod]` attribute. Such methods are, for example

  * `MoveForward()` and `CanMoveForward()`
  * `TurnLeft()` and `CanTurnLeft()`
  * `DoorInViewingDirectionVisited()`

etc.

### Selection, Mutation and cross-over

After each generation, probands will be chosen randomly for the set of winners and losers. Mutation and cross-over will then be applied after another dice roll.

### Maze generation

Three maze generators are implemented:

  * Recursive Backtracker (default)
  * Randomized Kruskal
  * Randomized Prim

## License

Copyright &copy; 2013 Markus Mayer &lt;widemeadows@gmail.com&gt;

MazeEvolution is licensed under the EUPL, Version 1.1 or - as soon they will be approved by the European Commission -
subsequent versions of the EUPL (the "Licence"); you may not use this work except in compliance with the Licence.
You may obtain a copy of the Licence at:

[http://joinup.ec.europa.eu/software/page/eupl/licence-eupl](http://joinup.ec.europa.eu/software/page/eupl/licence-eupl)

Unless required by applicable law or agreed to in writing, software distributed under the Licence is
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the Licence for the specific language governing permissions and limitations under the Licence.
