// BFS 算法(未完...)
function bfsSolver(tiles) {
    let queue = [{tiles: tiles, path: []}];
    let visited = new Set();
    visited.add(tiles.toString());

    while (queue.length > 0) {
        let node = queue.shift();
        let emptyIndex = node.tiles.indexOf(9);

        // 四個可能的移動方向：上，下，左，右
        let directions = [-1, 1, -3, 3];
        directions.forEach(dir => {
            let newIndex = emptyIndex + dir;
            if (isValidMove(emptyIndex, newIndex)) {
                let newTiles = [...node.tiles];
                [newTiles[emptyIndex], newTiles[newIndex]] = [newTiles[newIndex], newTiles[emptyIndex]];
                
                if (!visited.has(newTiles.toString())) {
                    visited.add(newTiles.toString());
                    let newPath = node.path.concat([newIndex]);
                    queue.push({tiles: newTiles, path: newPath});
                    
                    // Check if solved
                    if (isSolved(newTiles)) {
                        console.log('Solution found:', newPath);
                        displaySolution(newPath);
                        return;
                    }
                }
            }
        });
    }
    console.log('No solution found');
}

function isValidMove(oldIndex, newIndex) {
    // Prevent wrap-around in the puzzle
    let oldRow = Math.floor(oldIndex / 3);
    let newRow = Math.floor(newIndex / 3);
    return newIndex >= 0 && newIndex < 9 && Math.abs(oldRow - newRow) <= 1;
}

function isSolved(tiles) {
    for (let i = 0; i < tiles.length - 1; i++) {
        if (tiles[i] !== i + 1) return false;
    }
    return tiles[tiles.length - 1] === 9;
}

function displaySolution(path) {
    let solutionDisplay = document.getElementById('solution');
    solutionDisplay.innerHTML = 'Solution path: ' + path.join(' -> ');
}
