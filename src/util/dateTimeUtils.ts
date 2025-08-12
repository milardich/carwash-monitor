export function strDateTime(date: Date): string {
    const year = date.getFullYear();
    const month = date.getMonth() < 10 ? "0" + (date.getMonth() + 1) : date.getMonth();
    const day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    const hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    const minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    const second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

    const dateTime: string =
        year    + "-" + 
        month   + "-" + 
        day     + "T" + 
        hour    + ":" + 
        minute  + ":" + 
        second;

    return dateTime;
}

export function strDateTimeMidnight(date: Date): string {
    const year = date.getFullYear();
    const month = date.getMonth() < 10 ? "0" + (date.getMonth() + 1) : date.getMonth();
    const day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    const hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
    const minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
    const second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();

    const dateTime: string =
        year + "-" + month + "-" + day + "T" + 
        "00" + ":" + "00" + ":" + "00";

    return dateTime;
}

export function strDateTimeMonthBegin(date: Date): string {
    const year = date.getFullYear();
    const month = date.getMonth() < 10 ? "0" + (date.getMonth() + 1) : date.getMonth();
    const day = "01";
    const hour = "00";
    const minute = "00";
    const second = "00";

    const dateTime: string =
        year + "-" + month + "-" + day + "T" + 
        hour + ":" + minute + ":" + second;

    return dateTime;
}

export function getMonthName(monthNumber: number): string {
    const monthNames = ["January", "February", "March", "April", "May","June","July", "August", "September", "October", "November","December"];
    return monthNames[monthNumber];
}