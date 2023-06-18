
    $.ajax({
        url: "https://swapi.dev/api/people/"
        }).done((result) => {
        let temp = ""
    let number = 0
        $.each(result.results, (key, val) => {
        number += 1
        temp += "<tr>" +
        "<td>"+number+"</td>"+
        "<td>"+val.name+"</td>"+
        "<td>"+val.height+"</td>"+
        "<td>"+val.mass+"</td>"+
        "<td>"+val.hair_color+"</td>"+
        "<td>"+val.gender+"</td>"+
        "</tr>";
            })
        console.log(temp);
        $("#tabelSW tbody").html(temp);
        }).fail((error) => {
        console.log(error);
        })