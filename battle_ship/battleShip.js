// ships container
const shipCont = document.createElement('div');
shipCont.className = 'ship-container';
function dragstartHandler(ev) {
    // Add the target element's id to the data transfer object
    ev.dataTransfer.setData("text/plain", ev.target.className);
    ev.dataTransfer.effectAllowed = "move";
  }
function dragoverHandler(ev) {
    ev.preventDefault();
    ev.dataTransfer.dropEffect = "move";
  }
  function dropHandler(ev) {
    ev.preventDefault();
    // Get the id of the target and add the moved element to the target's DOM
    const data = ev.dataTransfer.getData("text/plain");
    ev.target.className = data;
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
const smallShip = document.createElement('div');
smallShip.draggable = 'true';
smallShip.className = 'blue box';
smallShip.ondragstart = dragstartHandler;
shipCont.append(smallShip);



function drawBoard(element,numRows,numCols){
    // drawing coordinates row
    const coorRow = document.createElement('div')
    coorRow.className = 'row'
    for (let i=0;i<numCols;i++){
        const newBox = document.createElement('div')
        newBox.className = 'box'
        i>0? newBox.innerText = i : ""
        newBox.style.border = 'none'

        coorRow.appendChild(newBox)
    }

    element.appendChild(coorRow)

    // drawing main grid
    for (let i=1;i<numRows;i++){
        const row = document.createElement('div')
        row.className = 'row'
        for (let j=0;j<numCols;j++){
            const newBox = document.createElement('div');
            newBox.className = 'box'
            if(j==0){
                newBox.innerText = String.fromCharCode(64+i)
                newBox.style.border='none'
            }
            // newBox.addEventListener('click',(e)=>{
            //     e.target.style.background = 'blue'
            // })
            newBox.ondrop = dropHandler;
            newBox.ondragover = dragoverHandler;
            row.appendChild(newBox)
        }
        element.appendChild(row)
    }



    return element
}
