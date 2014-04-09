    angular.module('tbot')
           .controller('LoggedInController', function($scope, $http, $window, asmxService )
                {
                    $scope.currentUser = 'This is the current user....';
                    _asmxService = asmxService;
                    _http = $http

                    asmxService.set_CSRF_Token().then(function(result)
                                                {
                                                    var userData = result.data.d;
                                                    console.log("HERE with userData!!!!");
                                                    console.log(userData);
                                                    if (userData == null)
                                                    {
                                                        $window.location.href = '/login?LoginReferer=/tbot_users';
                                                    }
                                                    else
                                                    {
                                                        $scope.userName = userData.UserName;
                                                        _userData = userData;
                                                    }
                                                });
                })
           /*.controller('users-controller', function($scope)
                {
                    window.ang =
                        {
                            scope       : $scope
                          //  element     : $element
                        };

                })*/;