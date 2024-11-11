document.addEventListener('DOMContentLoaded', function () {
    initializeTiles();
    shuffleTiles();
});

//初始化拼圖
function initializeTiles() {
    const container = document.getElementById('puzzle-container');
    container.innerHTML = '';
    for (let i = 0; i < 9; i++) {
        const tile = document.createElement('div');
        tile.className = 'tile' + (i === 8 ? ' hidden' : ''); //前八塊和最後一塊套用不同 css
        tile.style.backgroundPosition = `-${(i % 3) * 100}px -${Math.floor(i / 3) * 100}px`;
        container.appendChild(tile);
    }
    addEventListeners();
}

//每塊拼圖添加點擊移動效果的監聽器
function addEventListeners() {
    const tiles = document.querySelectorAll('.tile');
    tiles.forEach((tile, index) => {
        tile.addEventListener('click', () => swapTiles(index));
    });
}

//拼圖塊可否交換位置的邏輯和檢查
function swapTiles(index) {
    //獲取空白塊位置的索引，以便確定被點擊的拼圖塊是否與空白位置相鄰。
    const emptyTile = document.querySelector('.hidden');
    const clickedTile = document.getElementById('puzzle-container').children[index];
    const emptyIndex = Array.from(emptyTile.parentNode.children).indexOf(emptyTile);

    //檢查被點擊拼圖塊是否與空白塊相鄰
    //如果被點擊的拼圖塊的橫坐標和空白位置的橫坐標相差1，而縱坐標相同，或者橫坐標相同而縱坐標相差1，則被點擊的拼圖塊和空白位置是相鄰的，返回 true，否則返回 false。
    const clickedIndex = index;
    const emptyX = emptyIndex % 3;
    const emptyY = Math.floor(emptyIndex / 3);
    const clickedX = clickedIndex % 3;
    const clickedY = Math.floor(clickedIndex / 3);
    const isAdjacent = (Math.abs(emptyX - clickedX) === 1 && emptyY === clickedY) || (Math.abs(emptyY - clickedY) === 1 && emptyX === clickedX);

    // 使用解構賦值來交換背景圖位置
    if (isAdjacent) {
        [clickedTile.style.backgroundPosition, emptyTile.style.backgroundPosition] = 
        [emptyTile.style.backgroundPosition, clickedTile.style.backgroundPosition];
        clickedTile.classList.add('hidden');
        emptyTile.classList.remove('hidden');
        // 延遲檢查遊戲是否已通關，讓最後一塊拼圖移動完後再跳通知
        setTimeout(checkIfSolved, 100);
    }
}

//打亂拼圖遊戲，和透過計算保證最終拼圖遊戲是可解的
function shuffleTiles() {
    const container = document.getElementById('puzzle-container');
    let tiles = Array.from(container.children);
    // 獲取索引數組
    let positions = tiles.map((_, i) => i);

    /// 初始排列為不可解，使用 Fisher-Yates 洗牌算法打亂拼圖，直到計算為可解
    let isSolvable = false;
    while (!isSolvable) {
        for (let i = positions.length - 1; i > 0; i--) {
            const j = Math.floor(Math.random() * (i + 1));
            [positions[i], positions[j]] = [positions[j], positions[i]];
        }
        // 檢查拼圖遊戲是否是可解
        isSolvable = checkSolvable(positions);
    }

    // 打亂拼圖遊戲後，根據隱藏位置的索引，將最後一塊拼圖設置為隱藏
    positions.forEach((pos, index) => {
        tiles[index].style.backgroundPosition = `-${(pos % 3) * 100}px -${Math.floor(pos / 3) * 100}px`;
        if (pos === 8) {
            tiles[index].classList.add('hidden');
        } else {
            tiles[index].classList.remove('hidden');
        }
    });
}

//檢查拼圖的當前排列是否是可解的。通過計算逆序數的奇偶性來判斷一個排列是否可解。
function checkSolvable(positions) {
    let inversionCount = 0;
    const grid = positions.filter(pos => pos !== 8); 
    for (let i = 0; i < grid.length; i++) {
        for (let j = i + 1; j < grid.length; j++) {
            if (grid[i] > grid[j]) {
                inversionCount++;
            }
        }
    }
    return inversionCount % 2 === 0;
}

//只檢查前八塊拼圖塊是否已完成(排除最後一塊隱藏的拼圖塊)
function checkIfSolved() {
    const tiles = Array.from(document.getElementById('puzzle-container').children);
    // 檢查隱藏的拼圖塊是否是最後一块(索引 8)
    const hiddenTileIndex = tiles.findIndex(tile => tile.classList.contains('hidden'));
    const isHiddenTileLast = hiddenTileIndex === tiles.length - 1;
    
    // 檢查所有非隐藏的拼圖塊是否已在正確位置
    const isSolved = tiles.every((tile, index) => {
        // 忽略最後一塊隱藏的拼圖塊，否則無法完成遊戲
        if(tile.classList.contains('hidden')) {
            return true;
        }
        const correctX = -(index % 3) * 100;
        const correctY = -Math.floor(index / 3) * 100;
        const backgroundPos = tile.style.backgroundPosition.split(' ').map(pos => parseInt(pos));
        // 檢查當前(最後一個)拼圖塊是否在正確位置
        return backgroundPos[0] === correctX && backgroundPos[1] === correctY;
    });

    // 如果所有拼圖塊都在正確的位置，且隐藏的拼圖塊是最後一塊，那遊戲完成
    const status = document.getElementById('status');
    if (isSolved && isHiddenTileLast) {
        status.textContent = "拼圖完成!";
        alert("拼圖完成!");
    } else {
        status.textContent = "狀態: 遊戲中";
    }
}

/* 
Fisher-Yates 洗牌算法
1. 從最後一個元素開始，向前遍歷集合。
2. 在每個位置，隨機選擇一個索引，該索引必須在當前位置和開始位置之間。
3. 將當前位置的元素與隨機選擇的索引處的元素進行交換。
4. 重複上述步驟，直到所有元素都已經處理完畢。
*/

/*
逆序數是指在一個序列中，如果某個數大於其後面的數，則稱這兩個數構成一個逆序。如果拼圖的逆序數為偶數，則可以通過移動拼圖還原為原始狀態；如果為奇數，則無法還原。
*/
