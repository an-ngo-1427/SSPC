// ships container
const shipCont = document.createElement('div');
shipCont.className = 'ship-container';
function dragstartHandler(ev) {
    // Add the target element's id to the data transfer object
    dragged = ev.target;
    ev.dataTransfer.effectAllowed = "move";
    ev.width = '100px';
}
function dragoverHandler(ev) {
    ev.preventDefault();
}

function dropHandler(ev) {

  // Get the id of the target and add the moved element to the target's DOM

  ev.preventDefault();
  let selectedBoxes = [];
  let curr = ev.target;

  if(dragged.classList.contains('horizontal')){
    for(let i=0;i<dragged.children.length;i++){
      if(curr == null || curr.children.length || curr.classList.contains('blue')){
        selectedBoxes = [];
        break;
      }
      console.log(curr);
      selectedBoxes.push(curr);
      curr = curr.nextSibling;
    }

  }else if(dragged.classList.contains('vertical')){

    const verticalIdx = Array.prototype.indexOf.call(ev.target.parentNode.children,ev.target);
    let currRow = ev.target.parentNode;
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
const smallShip1 = drawShips('vertical',2);
const smallShip2 = drawShips('vertical',3);
const smallShip3 = drawShips('horizontal',2);
const smallShip4 = drawShips('horizontal',3);
shipCont.ondragstart = dragstartHandler;
shipCont.append(smallShip1,smallShip2,smallShip3,smallShip4);



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
