
// Defining angularjs module
var app = angular.module('commentModule', []);
// Defining angularjs Controller and injecting ProductsService
app.controller('commentCtrl', function ($scope, $http, CommentService) {
    $scope.commentData = null;
    // Fetching records from the factory created at the bottom of the script file
    CommentService.GetAllRecords().then(function (d) {
        $scope.commentData = d.data; // Success
    }, function () {
        alert('Error Occured !!!'); // Failed
    });
    // Calculate Total of Price After Initialization
    $scope.total = function () {
        var total = 0;
        var num = 0;
        angular.forEach($scope.commentData, function (item) {
            total += item.rating;
            num += 1;
        })
        return total/num;
    }
    $scope.Comment = {
        Id: '',
        Category: '',
        Name: '',
        Content: '',
        Price: '',
    };
    // Reset product details
    $scope.clear = function () {
        $scope.Comment.Id = '';
        $scope.Comment.articleId = '';
        $scope.Comment.comment1 = '';
        $scope.Comment.rating = '';
        $scope.Comment.name = '';
    }
    //Add New Item
    $scope.save = function () {
        if ($scope.Comment.articleId != "" &&
        $scope.Comment.rating != "" && $scope.Comment.name != "") {
            $http({
                method: 'POST',
                url: '/api/Comments/PostComment/',
                data: $scope.Comment
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.commentData.push(response.data);
                $scope.clear();
                alert("Comment Added Successfully !!!");
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };
    // Edit comment details
    $scope.edit = function (data) {
        $scope.Comment = { Id: data.Id, articleId: data.articleId, comment1: data.comment1, rating: data.rating, name: data.name };
    }
    // Cancel comment details
    $scope.cancel = function () {
        $scope.clear();
    }
    // Update product details
    $scope.update = function () {
        if ($scope.Comment.articleId != "" &&
        $scope.Comment.rating != "" && $scope.Comment.name != "" && $scope.Comment.comment1 != "") {
            $http({
                method: 'PUT',
                url: '/api/Comments/PutComment/' + $scope.Comment.Id,
                data: $scope.Comment
            }).then(function successCallback(response) {
                $scope.commentData = response.data;
                $scope.clear();
                alert("Comment Updated Successfully !!!");
            }, function errorCallback(response) {
                alert("Error : " + response.data.ExceptionMessage);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    };
    // Delete product details
    $scope.delete = function (index) {
        $http({
            method: 'DELETE',
            url: '/api/Comments/DeleteComment/' + $scope.commentData[index].Id,
        }).then(function successCallback(response) {
            $scope.commentData.splice(index, 1);
            alert("Comment Deleted Successfully !!!");
        }, function errorCallback(response) {
            alert("Error : " + response.data.ExceptionMessage);
        });
    };
});
// Here I have created a factory which is a popular way to create and configure services.
// You may also create the factories in another script file which is best practice.
app.factory('CommentService', function ($http) {
    var fac = {};
    fac.GetAllRecords = function () {
        return $http.get('/api/Comments/GetAllComments');
    }
    return fac;
});