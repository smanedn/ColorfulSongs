<?php

class Ajax
{
    public function getUsername($toSearch = null, $filter = null) {

        if ($filter == null){
            $filter = 'global';
        }

        $username = $this->search($toSearch,$filter);

        if ($username) {
            header('Content-Type: application/json');
            echo json_encode($username);
        } else {
            echo json_encode(array('error' => 'Username not found'));
        }

    }

    private function search($field, $filter){
        $currentYear = date("Y");
        if ($field == null || $field == "*" || $filter == 'global') {
            return Leaderboard::getData();
        } else {
            return Leaderboard::getDataByUsername($field);
        }
    }

}