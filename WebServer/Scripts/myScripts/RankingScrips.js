var ViewModel = function () {
    var self = this; // make 'this' available to subfunctions or closures

    self.Ranks = ko.observableArray(); // enables data binding
    self.rank = ko.observableArray();
    var RanksUri = '/api/Ranks/';
    function getRanks() {
        $.getJSON(RanksUri).done(function (data) {
            data.sort(function (a, b) { return (b.GamesWon - b.GamesLost) - (a.GamesWon - a.GamesLost) });
            rank = 1;
            data.forEach(function (user) {
                user.Rank = rank;
                rank = rank + 1;
            }

            );
            self.Ranks(data);
        });
    }
    // Fetch the initial data
    getRanks();
};
ko.applyBindings(new ViewModel());
