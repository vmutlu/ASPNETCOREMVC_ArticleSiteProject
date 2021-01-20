function convertFirstLetterToUpperCase(text) { //aldıgı metnin ilk harfinin büyük yapıp geri döner
    return text.charAt(0).toUpperCase() + text.slice(1);
}

function convertToShortDate(dateString) {
    const shortDate = new Date(dateString).toLocaleDateString('en-US');
    return shortDate;
}