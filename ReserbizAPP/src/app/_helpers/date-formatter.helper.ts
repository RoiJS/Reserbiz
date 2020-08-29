export class DateFormatter {
  static format(date: Date) {
    if (!date) {
      return '';
    }

    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();

    return `${year}-${month}-${day}`;
  }
}
