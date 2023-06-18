
const animals = [
    { name: "nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "gary", species: "mouse", class: { name: "mamalia" } },
    { name: "dory", species: "fish", class: { name: "invertebrata" } },
    { name: "tom", species: "mouse", class: { name: "mamalia" } },
    { name: "aji", species: "wibu", class: { name: "mamalia" } }
]

animals.forEach(animal => {
    if (animal.species != "mouse") {
        animal.class = "Bukan Mamalia"
    }
})

const onlymouse = []
animals.forEach(animal => {
    if (animal.species == "mouse") {
        onlymouse.push(animal)
    }
})

console.log(animals)
console.log(onlymouse)