const list = document.querySelector(".list")

const prependBut = document.querySelector('#add-front')

function createItem(){
    const text = 'new item'
    newItem = document.createElement('li')
    newItem.innerText = text
    return newItem
}
prependBut.addEventListener('click',event=>{
    const listItem = createItem()
    list.prepend(listItem)
})


const appendBut = document.querySelector('#add-end')
appendBut.addEventListener('click',e=>{
    const listItem = createItem()
    list.append(listItem)
})
