function changeDirection(direction, lang) {
    if (lang == 'vi') {
        var data = direction.split(',');
        var array = [];
        for (var i = 0; i < data.length; i++) {
            switch (direction) {
                case 'North':
                    array.push("Bắc");
                    break;
                case 'Northeast':
                    array.push("Đông Bắc");
                    break;
                case 'East':
                    array.push("Đông");
                    break;
                case 'Southeast':
                    array.push("Đông Nam");
                    break;
                case 'South':
                    array.push("Nam");
                    break;
                case 'Southwest':
                    array.push("Tây Nam");
                    break;
                case 'West':
                    array.push("Tây");
                    break;
                case 'Northwest':
                    array.push("Tây Bắc");
                    break;
                default:
                    break;

            }
        }

        return array.join(', ');
    }
    return direction;

}