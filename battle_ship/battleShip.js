// ships container
const shipCont = document.createElement('div');
shipCont.className = 'ship-container';
function dragstartHandler(ev) {
    // Add the target element's id to the data transfer object
    dragged = ev.target;
    ev.dataTransfer.effectAllowed = "move";
    const dataObj = JSON.stringify({
      "offsetX":ev.offsetX,
      "offsetY":ev.offsetY
    })
    ev.dataTransfer.setData('dataObj',dataObj)

    console.log(dragged);
}
function dragoverHandler(ev) {
    ev.preventDefault();
}

function dropHandler(ev) {

  // Get the id of the target and add the moved element to the target's DOM
  const data = JSON.parse(ev.dataTransfer.getData('dataObj'));

  ev.preventDefault();
  let selectedBoxes = [];
  let curr;

  if(dragged.classList.contains('horizontal')){
    const boxWidth = dragged.children[0].offsetWidth;
    if(data.offsetX<boxWidth) curr = ev.target;
    else if(data.offsetX < 2 * boxWidth) curr = ev.target.previousSibling;
    else curr = ev.target?.previousSibling?.previousSibling;

    for(let i=0;i<dragged.children.length;i++){
      if(curr == null || curr.children.length || curr.classList.contains('blue')){
        selectedBoxes = [];
        break;
      }
      selectedBoxes.push(curr);
      curr = curr.nextSibling;
    }

  }else if(dragged.classList.contains('vertical')){
    const boxHeigth = dragged.children[0].offsetHeight;
    const verticalIdx = Array.prototype.indexOf.call(ev.target.parentNode.children,ev.target);
    let currRow;
    if(data.offsetY < boxHeigth) currRow = ev.target.parentNode;
    else if(data.offsetY < boxHeigth * 2) currRow = ev.target.parentNode.previousSibling;
    else currRow = ev.target.parentNode.previousSibling?.previousSibling;

    for(let i=0;i<dragged.children.length;i++){
      if(currRow == null || currRow.children[verticalIdx].children.length || currRow.children[verticalIdx].classList.contains('blue')){
        selectedBoxes = [];
        break;
      }
      selectedBoxes.push(currRow.children[verticalIdx]);
      currRow = currRow.nextSibling;
    }
  }

  selectedBoxes.forEach(ele=>ele.appendChild(dragged.children[0]))

}


// player board
const self_field = document.createElement('div');
self_field.className = 'self-field';
drawBoard(self_field,8,8)
// opponent board
const op_field = document.createElement('div');
op_field.className = 'op-field';
drawBoard(op_field,8,8)


const body = document.querySelector('body');
body.append(shipCont,self_field,op_field);

// adding drag and drop elements
// players ships
const smallShip1 = drawShips('vertical',2);
const smallShip2 = drawShips('vertical',3);
const smallShip3 = drawShips('horizontal',2);
const smallShip4 = drawShips('horizontal',3);

// comp ships
// shipCont.ondragstart = dragstartHandler;
shipCont.append(smallShip1,smallShip2,smallShip3,smallShip4);
// shipCont1.append(smallShip5,smallShip6,smallShip7,smallShip8);



// creating dragable ships helper
function drawShips(orientation,eleNums){
  const newShip = document.createElement('div');
  newShip.className = `ship ${orientation}`;
  newShip.draggable = 'true';
  for (let i=0;i<eleNums;i++){
    const newBox = document.createElement('div');
    newBox.className = 'blue box';
    newShip.appendChild(newBox);
  }
  newShip.ondragstart = dragstartHandler;
  return newShip;
}

function drawBoard(element,numRows,numCols){
  // drawing coordinates row
  const coorRow = document.createElement('div')
  coorRow.className = 'row'
  for (let i=0;i<numCols;i++){
    const newBox = document.createElement('div')
    newBox.className = 'box'
    i>0? newBox.innerText = i : ""
    newBox.style.border = 'none';
    coorRow.appendChild(newBox)
  }

  element.appendChild(coorRow)

  // drawing main grid
  for (let i=1;i<numRows;i++){
    const row = document.createElement('div')
    row.className = 'row'
    for (let j=0;j<numCols;j++){
      const newBox = document.createElement('div');
      newBox.className = 'box';
      if(j==0){
        newBox.innerText = String.fromCharCode(64+i)
        newBox.style.border='none'
      }
      newBox.ondrop = dropHandler;
      newBox.ondragover = dragoverHandler;
      row.appendChild(newBox)
    }
    element.appendChild(row)
  }


  return element
}
function compPlay(){
  let count = 0;
  const fieldRow = op_field?.children?.length;
  const fieldCol = op_field?.children?.length;
  console.log(fieldCol,fieldRow);
  while(count <= 3){
    const rowCoor = Math.floor(Math.random() * (fieldRow - 1) + 1);
    const colCoor = Math.floor(Math.random() * (fieldCol - 1) + 1);
    const orientation = ['horizontal','vertical'][Math.floor(Math.random() * (2))];
    // const orientation = 'vertical';
    const shipLength = Math.floor(Math.random() * (4-2) + 2);
    let compCurr = op_field?.children[rowCoor]?.children[colCoor];
    if(compCurr.className == 'red') continue;
    console.log(compCurr.className);
    let selectedBox = []
    for( let i=0;i<shipLength;i++){

      if(compCurr == null){
        selectedBox = [];
        break;
      }
      selectedBox.push(compCurr)
      if(orientation == 'horizontal'){
        console.log(compCurr,'horizontal')
        compCurr = compCurr.nextSibling;
      }else if(orientation == 'vertical'){
        console.log(compCurr,'vertical')
        const currIdx = Array.prototype.indexOf.call(compCurr.parentNode.children,compCurr);
        compCurr = compCurr.parentNode.nextSibling? compCurr.parentNode.nextSibling.children[currIdx]:null;
      }
    }
    console.log(selectedBox,shipLength)
    if(selectedBox.length == shipLength){
      selectedBox.forEach(box=>box.classList.add('red'));
      count++;
    }
  }

}
compPlay();
