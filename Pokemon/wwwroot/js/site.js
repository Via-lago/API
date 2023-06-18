/*$.ajax({
    url: ""
}).done((result) => {
    let temp = ""
    let number = 0
    $.each(result.results, (key, val) => {
        number += 1
        temp += `<tr> +
        <td>${number}</td>
        <td>${val.name}</td>
        <td><button onclick="detail('${val.url}')" type="button" class="btn btn-dark" data-bs-toggle="modal" data-bs-target="#detail">Detail</button></td>
        </tr>`;
    })
     console.log(temp);
    $("#tabelSW tbody").html(temp);
}).fail((error) => {
    console.log(error);
})*/

$(document).ready(function () {
    $('#Employee').DataTable({
        ajax: {
            url: "https://localhost:7274/api/Employee",
            dataSrc: "data",
            dataType: "JSON"
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "guid" },
            { data: "nik" },
            { data: "firstName" },
            { data: "lastName" },
            { data: "birthDate" },
            { data: "gender" },
            { data: "hiringDate" },
            { data: "email" },
            { data: "phoneNumber" },
            {
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#employeeUpdateModal" onclick="openUpdateEmployeeModal('${row?.guid}', '${row?.nik}', '${row?.firstName}', '${row?.lastName}', '${row.birthDate}', '${row.gender}', '${row.hiringDate}', '${row.email}', '${row.phoneNumber}')">Update</button>`;
                }
            },
            {
                render: function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-danger" onclick="deleteEmployee('${row.guid}')">Delete</button>`;
                }
            }
        ]
    });
});

function AddEmployee() {
    var eNik = $('#employee-nik').val();
    var eFirst = $('#employee-fname').val();
    var eLast = $('#employee-lname').val();
    var eBDate = $('#employee-bdate').val();
    var eGender = $('#employee-gender').val();
    var eHDate = $('#employee-hdate').val();
    var eEmail = $('#employee-email').val();
    var ePhone = $('#employee-pnumber').val();

    $.ajax({
        async: true, // Async by default is set to “true” load the script asynchronously  
        // URL to post data into sharepoint list  
        url: "https://localhost:7274/api/Employee",
        method: "POST", //Specifies the operation to create the list item  
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem' // it defines the ListEnitityTypeName  
            },
            //Pass the parameters
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'hiringDate': eHDate,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose", //It defines the Data format   
            "content-type": "application/json;odata=verbose", //It defines the content type as JSON  
/*            "X-RequestDigest": $("#__REQUESTDIGEST").val() //It gets the digest value   
*/        },
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(JSON.stringify(error));

        }

    })

}

function openUpdateEmployeeModal(guid, nik, firstName, lastName, birthDate, gender, hiringDate, email, phoneNumber) {
    // Set the values of the input fields in the modal
    /*console.log('woi ini dari update: ', nik)*/
    document.getElementById('u-employee-nik').value = nik;
    document.getElementById('u-employee-fname').value = firstName;
    document.getElementById('u-employee-lname').value = lastName;
    document.getElementById('u-employee-bdate').value = birthDate;
    document.getElementById('u-employee-hdate').value = hiringDate;
    document.getElementById('u-employee-email').value = email;
    document.getElementById('u-employee-pnumber').value = phoneNumber;

    // Set the gender radio button based on the gender value
    if (gender === 1) {
        document.getElementById('u-employee-gender-m').checked = true;
    } else {
        document.getElementById('u-employee-gender-f').checked = true;
    }

    // Change the modal title
    document.getElementById('employeeUpdateModalTitle').innerText = 'Update Employee';

    // Show the modal
    $('#employeeUpdateModal').modal('show');

    // Add an event listener to the form submit button for updating the employee
    document.getElementById('employeeUpdateModalBody').addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent form submission

        // Call the updateEmployee function with the GUID parameter
        updateEmployee(guid);
    });
}

function updateEmployee(guid) {
    console.log('this guid: ', guid)
    // Retrieve the updated values from the input fields
    var eNik = document.getElementById('u-employee-nik').value;
    var eFirst = document.getElementById('u-employee-fname').value;
    var eLast = document.getElementById('u-employee-lname').value;
    var eBDate = document.getElementById('u-employee-bdate').value;
    var eGender = document.querySelector('input[name="u-employee-gender"]:checked').id.includes('m') ? 1 : 0;
    var eHDate = document.getElementById('u-employee-hdate').value;
    var eEmail = document.getElementById('u-employee-email').value;
    var ePhone = document.getElementById('u-employee-pnumber').value;

    $.ajax({
        url: `https://localhost:7274/api/Employee`,
        method: "PUT",
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem'
            },
            'guid': guid,
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'hiringDate': eHDate,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
            "X-HTTP-Method": "MERGE",
            "IF-MATCH": "*"
        },
        success: function (data) {
            console.log(data);
            window.location.reload()
            // Get the updated row data as an array
            var updatedRowData = [
                eNik,
                eFirst,
                eLast,
                eBDate,
                eGender === 1 ? 'Male' : 'Female',
                eHDate,
                eEmail,
                ePhone
                // Add other columns as needed
            ];

            // Loop through each row in the DataTable
            dataTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var rowData = this.data();

                // Check if the GUID in the row matches the guid parameter
                if (rowData[0] === guid) {
                    // Update the row data
                    this.data(updatedRowData);

                    // Redraw the updated row
                    this.invalidate();

                    // Exit the loop
                    return false;
                }
            });

            // Close the modal
            $('#employeeUpdateModal').modal('hide');
        },
        error: function (error) {
            console.log("Error: " + JSON.stringify(error));
        }
    });
}
/*$(document).ready(function () {
    let table = $("#tabelSW").DataTable({
        ajax: {
            url: "https://pokeapi.co/api/v2/pokemon?limit=100000&offset=0",
            dataSrc: "results",
            dataType: "JSON"
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "name" },
            {
                data: "url",
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#modalDetail" onclick="detail('${data}')">Detail</button>`;
                }
            },
            {
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#employeeModal" onclick="openAddEmpModal()">Employee</button>`;


                }
            },
            {
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#pendaftaranModal" onclick="openPendaftaranModal()">Pendaftaran</button>`;
                }
            }
        ]
    });
});

function openAddEmpModal() {
    $("#employeeModal").modal("show");
} 

function openPendaftaranModal() {
    $("#pendaftaranModal").modal("show");
}


(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()

function removeBar() {
    $(".container-progress").empty();
}
function detail(url) {
    $.ajax({
        url: url,
    }).done(res => {
        console.log(res);
        $(".nama").html(res.name)
        $("#imgPoke").attr("src", res.sprites.other['official-artwork'].front_default)
        $("#imgPoke2").attr("src", res.sprites.other['official-artwork'].front_shiny)

        $("#height").html(res.height)
        $("#weight").html(res.weight)
        $("#experience").html(res.base_experience)


        $.ajax({
            url: res.species.url
        }).done((species) => {
            const description = species.flavor_text_entries.find(entry => entry.language.name === "en");
            $("#description").text(description.flavor_text);
        }).fail((error) => {
            console.log(error);
        });

        let abilitiesList = '<ul>';
        res.abilities.forEach(ability => {
            abilitiesList += `<li>${ability.ability.name}</li>`;
        });
        abilitiesList += '</ul>';
        $("#ability").html(abilitiesList);


        let temp = "";
        $.each(res.types, (key, val) => {
            const type = val.type.name;
            const badgeSize = "badge-width-1500px";
            temp += `<span class="badge ${type} ${badgeSize}">${type}</span> `;
        });
        $("#badgee").html(temp);

        const baseStats = res.stats;
        console.log(baseStats);

        const hp = baseStats[0].base_stat;
        const attack = baseStats[1].base_stat;
        const defense = baseStats[2].base_stat;
        const spattack = baseStats[3].base_stat;
        const spdefense = baseStats[4].base_stat;
        const speed = baseStats[5].base_stat;

        console.log("HP: ", hp);
        console.log("Attack: ", attack);
        console.log("Defense: ", defense);
        console.log("Special Attack: ", spattack);
        console.log("Special Defense: ", spdefense);
        console.log("Speed: ", speed);

        const progressBar = [
            { name: "Hp", stat: baseStats[0].base_stat },
            { name: "Attack", stat: baseStats[1].base_stat },
            { name: "Defense", stat: baseStats[2].base_stat },
            { name: "Special Attack", stat: baseStats[3].base_stat },
            { name: "Special Defense", stat: baseStats[4].base_stat },
            { name: "Speed", stat: baseStats[5].base_stat },

        ];
        $(".container-progress").empty();

        $.each(progressBar, function (index, progressBar) {
            var progress = $("<div>").addClass("progress mx-auto mb-3");
            var progressBarInner = $("<div>")
                .addClass("progress-bar")
                .attr({
                    role: "progressbar",
                    style: "width: " + progressBar.stat + "%; background-color: " + getProgressBarColor(progressBar.stat),
                    "aria-valuenow": progressBar.stat,
                    "aria-valuemin": "0",
                    "aria-valuemax": "200"
                })
                .text(progressBar.name + ": " + progressBar.stat);
            progress.append(progressBarInner);
            $(".container-progress").append(progress);

        });
    })


    function getProgressBarColor(stat) {
        if (stat >= 70) {
            return "green"; // High stat, green color
        } else if (stat >= 40) {
            return "orange"; // Medium stat, orange color
        } else {
            return "red"; // Low stat, red color
        }
    }
}*/