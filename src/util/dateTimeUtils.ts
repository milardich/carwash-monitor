export function strDateTime(date: Date): string {
    var year = date.getFullYear();
    var month = date.getMonth() < 10 ? "0" + (date.getMonth() + 1) : date.getMonth();
    var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

    var dateTime: string =
        year    + "-" + 
        month   + "-" + 
        day     + "T" + 
        hour    + ":" + 
        minute  + ":" + 
        second;

    return dateTime;
}

export function strDateTimeMidnight(date: Date): string {
    var year = date.getFullYear();
    var month = date.getMonth() < 10 ? "0" + (date.getMonth() + 1) : date.getMonth();
    var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    var hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    var minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    var second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

    var dateTime: string =
        year + "-" + month + "-" + day + "T" + 
        "00" + ":" + "00" + ":" + "00";

    return dateTime;
}

export function strDateTimeMonthBegin(date: Date): string {
    var year = date.getFullYear();
    var month = date.getMonth() < 10 ? "0" + (date.getMonth() + 1) : date.getMonth();
    var day = "01";
    var hour = "00";
    var minute = "00";
    var second = "00";

    var dateTime: string =
        year + "-" + month + "-" + day + "T" + 
        hour + ":" + minute + ":" + second;

    return dateTime;
}

export function getMonthName(monthNumber: number): string {
    var monthNames = ["January", "February", "March", "April", "May","June","July", "August", "September", "October", "November","December"];
    return monthNames[monthNumber];
}