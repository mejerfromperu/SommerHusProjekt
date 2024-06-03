document.getElementById('redigerButton').addEventListener('click', function () {
    var inputFirstName = document.getElementById('inputFirstName');
    var inputLastName = document.getElementById('inputLastName');
    var inputPhone = document.getElementById('inputPhone');
    var inputEmail = document.getElementById('inputEmail');
    var inputStreetName = document.getElementById('inputStreetName');
    var inputHouseNumber = document.getElementById('inputHouseNumber');
    var inputFloor = document.getElementById('inputFloor');
    var inputPostalcode = document.getElementById('inputPostalcode');
    var inputLandlord = document.getElementById('inputLandlord')
    var inputCity = document.getElementById('inputCity')

    var outputFirstName = document.getElementById('outputFirstName');
    var outputLastName = document.getElementById('outputLastName');
    var outputPhone = document.getElementById('outputPhone');
    var outputEmail = document.getElementById('outputEmail');
    var outputStreetName = document.getElementById('outputStreetName');
    var outputHouseNumber = document.getElementById('outputHouseNumber');
    var outputFloor = document.getElementById('outputFloor');
    var outputPostalcode = document.getElementById('outputPostalcode');
    var outputLandlord = document.getElementById('outputLandlord');
    var outputCity = document.getElementById('outputCity')


    if (inputFirstName.style.display === 'none') {
        inputFirstName.style.display = 'block';
        inputLastName.style.display = 'block';
        inputPhone.style.display = 'block';
        inputEmail.style.display = 'block';
        inputStreetName.style.display = 'block';
        inputHouseNumber.style.display = 'block';
        inputFloor.style.display = 'block';
        inputPostalcode.style.display = 'block';
        inputLandlord.style.display = 'block';

        outputFirstName.style.display = 'none';
        outputLastName.style.display = 'none';
        outputPhone.style.display = 'none';
        outputEmail.style.display = 'none';
        outputStreetName.style.display = 'none';
        outputHouseNumber.style.display = 'none';
        outputFloor.style.display = 'none';
        outputPostalcode.style.display = 'none';
        outputLandlord.style.display = 'none';
    }
    else {
        inputFirstName.style.display = 'none';
        inputLastName.style.display = 'none';
        inputPhone.style.display = 'none';
        inputEmail.style.display = 'none';
        inputStreetName.style.display = 'none';
        inputHouseNumber.style.display = 'none';
        inputFloor.style.display = 'none';
        inputPostalcode.style.display = 'none';
        inputLandlord.style.display = 'none';

        outputFirstName.style.display = 'block';
        outputLastName.style.display = 'block';
        outputPhone.style.display = 'block';
        outputEmail.style.display = 'block';
        outputStreetName.style.display = 'block';
        outputHouseNumber.style.display = 'block';
        outputFloor.style.display = 'block';
        outputPostalcode.style.display = 'block';
        outputLandlord.style.display = 'block';
    }
});